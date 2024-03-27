using Domain.Entities.Clusterization;
using Domain.Entities.ExternalData;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Tasks;
using Domain.LoadHelpModels;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Text;
using System.Web;

namespace Domain.Services.Embeddings
{
    public class LoadEmbeddingsService : ILoadEmbeddingsService
    {
        private readonly IRepository<Comment> comment_repository;
        private readonly IRepository<ExternalObject> externalObjects_repository;
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<ClusterizationWorkspace> workspace_repository;
        private readonly IClusterizationDimensionTypesService DimensionTypeService;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;
        private readonly IEmbeddingsService embeddingsService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;
        private readonly IUserService _userService;

        private readonly string apiKey;

        readonly string model = "text-embedding-ada-002";
        readonly string apiUrl = "https://api.openai.com/v1/embeddings";

        public LoadEmbeddingsService(IRepository<Comment> comment_repository,
                                     IRepository<ExternalObject> externalObjects_repository,
                                     IClusterizationDimensionTypesService DimensionTypeService,
                                     IRepository<ClusterizationWorkspace> workspace_repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IEmbeddingsService embeddingsService,
                                     IBackgroundJobClient backgroundJobClient,
                                     IMyTasksService taskService,
                                     IRepository<ClusterizationEntity> entities_repository,
                                     IUserService userService,
                                     IStringLocalizer<TaskTitles> tasksLocalizer)
        {
            this.comment_repository = comment_repository;
            this.externalObjects_repository = externalObjects_repository;
            this.DimensionTypeService = DimensionTypeService;
            this.workspace_repository = workspace_repository;
            this.localizer = localizer;
            this.backgroundJobClient = backgroundJobClient;
            this.taskService = taskService;
            _userService = userService;

            var openAIOptions = configuration.GetSection("OpenAIOptions");

            this.apiKey = openAIOptions["ApiKey"];
            this.embeddingsService = embeddingsService;
            this.entities_repository = entities_repository;
            _tasksLocalizer = tasksLocalizer;
        }
        public async Task LoadEmbeddingsByWorkspace(int workspaceId)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var taskId = await taskService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingEmbeddings]);

            backgroundJobClient.Enqueue(() => LoadEmbeddingsByWorkspaceBackgroundJob(workspaceId, userId, taskId));
        }
        public async Task LoadEmbeddingsByWorkspaceBackgroundJob(int workspaceId, string userId, int taskId)
        {
            var workspace = (await workspace_repository.GetAsync(c => c.Id == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Entities)}")).FirstOrDefault();

            if (workspace == null || (workspace.ChangingType == ChangingTypes.OnlyOwner && workspace.OwnerId != userId)) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            if (workspace.IsAllDataEmbedded) return;

            var stateId = await taskService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                if (workspace.TypeId == ClusterizationTypes.Comments)
                {
                    await LoadEmbeddingsToComments(taskId, workspace);
                }
                else if (workspace.TypeId == ClusterizationTypes.External)
                {
                    await LoadEmbeddingsToExternalData(taskId, workspace);
                }
                
                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
                await taskService.ChangeTaskDescription(taskId, ex.Message);
            }
        }
        public async Task LoadEmbeddingsToComments(int taskId, ClusterizationWorkspace workspace)
        {
            var Dimensions = (await DimensionTypeService.GetAll()).ToList();

            var comments = await comment_repository.GetAsync(c => c.Workspaces.Contains(workspace), includeProperties: $"{nameof(Comment.Workspaces)},{nameof(Comment.EmbeddingData)}");
            foreach (var entity in workspace.Entities)
            {
                entities_repository.Remove(entity);
            }
            workspace.Entities.Clear();

            float percent = 0f;

            foreach (var comment in comments)
            {
                percent += 100f / comments.Count();
                await taskService.ChangeTaskPercent(taskId, percent);

                if (comment.EmbeddingData != null)
                {
                    var entity = new ClusterizationEntity()
                    {
                        Comment = comment,
                        TextValue = comment.TextDisplay,
                        EmbeddingData = comment.EmbeddingData,
                        WorkspaceId = workspace.Id
                    };
                    workspace.Entities.Add(entity);
                    await entities_repository.AddAsync(entity);

                    continue;
                }
                var text = comment.TextOriginal;
                if (text.Length > 2000) text = text.Substring(0, 2000);
                var inputText = HttpUtility.UrlEncode(text);

                string requestBody = $"{{\"input\": \"{inputText}\", \"model\": \"{model}\"}}";

                using (var httpClient = new HttpClient())
                {
                    // Set the "Authorization" header
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    // Create the HTTP request
                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    // Set the "Content-Type" header on the content
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(apiUrl, content);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<EmbeddingDataLoadModel>(responseContent);

                        // Extract the embeddings as a List<float>
                        List<double> embeddings = responseObject.Data[0].Embedding;


                        for (int i = 0; i < Dimensions.Count; i++)
                        {
                            if (Dimensions[i].DimensionCount == 1536)
                            {
                                await embeddingsService.AddEmbeddingToComment(embeddings.ToArray(), 1536, comment);
                            }
                            else
                            {
                                var res = await JohnsonLindenstraussProjection(embeddings.ToArray(), 1536, Dimensions[i].DimensionCount);

                                await embeddingsService.AddEmbeddingToComment(res, Dimensions[i].DimensionCount, comment);
                            }
                        }

                        var entity = new ClusterizationEntity()
                        {
                            Comment = comment,
                            EmbeddingData = comment.EmbeddingData,
                            WorkspaceId = workspace.Id,
                            TextValue = comment.TextDisplay
                        };
                        workspace.Entities.Add(entity);
                        await entities_repository.AddAsync(entity);
                    }
                    else
                    {
                        throw new HttpException(localizer[ErrorMessagePatterns.EmbeddingsLoadingError], response.StatusCode);
                    }
                }
            }

            workspace.IsAllDataEmbedded = true;
            await workspace_repository.SaveChangesAsync();
        }
        public async Task LoadEmbeddingsToExternalData(int taskId, ClusterizationWorkspace workspace)
        {
            var Dimensions = (await DimensionTypeService.GetAll()).ToList();

            var externalObjects = await externalObjects_repository.GetAsync(c => c.Workspaces.Contains(workspace), includeProperties: $"{nameof(ExternalObject.Workspaces)},{nameof(ExternalObject.EmbeddingData)}");
            foreach (var entity in workspace.Entities)
            {
                entities_repository.Remove(entity);
            }
            workspace.Entities.Clear();

            float percent = 0f;

            foreach (var externalObj in externalObjects)
            {
                percent += 100f / externalObjects.Count();
                await taskService.ChangeTaskPercent(taskId, percent);

                if (externalObj.EmbeddingData != null)
                {
                    var entity = new ClusterizationEntity()
                    {
                        ExternalObject = externalObj,
                        TextValue = externalObj.Text,
                        EmbeddingData = externalObj.EmbeddingData,
                        WorkspaceId = workspace.Id
                    };
                    workspace.Entities.Add(entity);
                    await entities_repository.AddAsync(entity);

                    continue;
                }

                var inputText = HttpUtility.UrlEncodeUnicode(externalObj.Text);

                string requestBody = $"{{\"input\": \"{inputText}\", \"model\": \"{model}\"}}";

                using (var httpClient = new HttpClient())
                {
                    // Set the "Authorization" header
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    // Create the HTTP request
                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    // Set the "Content-Type" header on the content
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(apiUrl, content);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<EmbeddingDataLoadModel>(responseContent);

                        // Extract the embeddings as a List<float>
                        List<double> embeddings = responseObject.Data[0].Embedding;


                        for (int i = 0; i < Dimensions.Count; i++)
                        {
                            if (Dimensions[i].DimensionCount == 1536)
                            {
                                await embeddingsService.AddEmbeddingToExternalObject(embeddings.ToArray(), 1536, externalObj);
                            }
                            else
                            {
                                var res = await JohnsonLindenstraussProjection(embeddings.ToArray(), 1536, Dimensions[i].DimensionCount);

                                await embeddingsService.AddEmbeddingToExternalObject(res, Dimensions[i].DimensionCount, externalObj);
                            }
                        }

                        var entity = new ClusterizationEntity()
                        {
                            ExternalObject = externalObj,
                            EmbeddingData = externalObj.EmbeddingData,
                            WorkspaceId = workspace.Id,
                            TextValue = externalObj.Text
                        };
                        workspace.Entities.Add(entity);
                        await entities_repository.AddAsync(entity);
                    }
                    else
                    {
                        throw new HttpException(localizer[ErrorMessagePatterns.EmbeddingsLoadingError], response.StatusCode);
                    }
                }
            }

            workspace.IsAllDataEmbedded = true;
            await workspace_repository.SaveChangesAsync();
        }

        private async Task<double[]> JohnsonLindenstraussProjection(double[] data, int originalDimension, int targetDimension)
        {
            double epsilon = 0.1; // Adjust this value as needed
            int numSamples = (int)Math.Ceiling(Math.Log(originalDimension) / epsilon / epsilon);

            double[] projectionMatrix = new double[targetDimension * originalDimension];
            Random random = new Random();

            for (int i = 0; i < targetDimension * originalDimension; i++)
            {
                projectionMatrix[i] = (random.NextDouble() < 0.5 ? 1 : -1) / Math.Sqrt(numSamples);
            }

            double[] projectedData = new double[data.Length / originalDimension * targetDimension];

            for (int i = 0; i < data.Length; i += originalDimension)
            {
                for (int j = 0; j < targetDimension; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < originalDimension; k++)
                    {
                        sum += data[i + k] * projectionMatrix[j * originalDimension + k];
                    }
                    projectedData[(i / originalDimension) * targetDimension + j] = sum;
                }
            }

            return projectedData;
        }
    }
}
