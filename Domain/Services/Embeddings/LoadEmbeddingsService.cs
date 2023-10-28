using Domain.Entities.Clusterization;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Tasks;
using Domain.LoadHelpModels;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Services.TaskServices;
using Hangfire;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Services.Embeddings
{
    public class LoadEmbeddingsService : ILoadEmbeddingsService
    {
        private readonly IRepository<Comment> comment_repository;
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<ClusterizationWorkspace> workspace_repository;
        private readonly IClusterizationDimensionTypesService DimensionTypeService;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IEmbeddingsService embeddingsService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;

        private readonly string apiKey;

        public LoadEmbeddingsService(IRepository<Comment> comment_repository,
                                     IClusterizationDimensionTypesService DimensionTypeService,
                                     IRepository<ClusterizationWorkspace> workspace_repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IEmbeddingsService embeddingsService,
                                     IBackgroundJobClient backgroundJobClient,
                                     IMyTasksService taskService,
                                     IRepository<ClusterizationEntity> entities_repository)
        {
            this.comment_repository = comment_repository;
            this.DimensionTypeService = DimensionTypeService;
            this.workspace_repository = workspace_repository;
            this.localizer = localizer;
            this.backgroundJobClient = backgroundJobClient;
            this.taskService = taskService;

            var openAIOptions = configuration.GetSection("OpenAIOptions");

            this.apiKey = openAIOptions["ApiKey"];
            this.embeddingsService = embeddingsService;
            this.entities_repository = entities_repository;
        }
        public async Task LoadEmbeddingsByWorkspace(int workspaceId)
        {
            backgroundJobClient.Enqueue(() => LoadEmbeddingsByWorkspaceBackgroundJob(workspaceId));
        }
        public async Task LoadEmbeddingsByWorkspaceBackgroundJob(int workspaceId)
        {
            var workspace = (await workspace_repository.GetAsync(c => c.Id == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            if (workspace.IsAllDataEmbedded) return;


            string model = "text-embedding-ada-002";
            string apiUrl = "https://api.openai.com/v1/embeddings";

            var Dimensions = (await DimensionTypeService.GetAll()).ToList();

            var comments = await comment_repository.GetAsync(c => c.Workspaces.Contains(workspace), includeProperties: $"{nameof(Comment.Workspaces)},{nameof(Comment.EmbeddingData)}");

            var taskId = await taskService.CreateTask("Завантаження ембедингів");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);
            try
            {
                foreach(var entity in workspace.Entities)
                {
                    entities_repository.Remove(entity);
                }
                workspace.Entities.Clear();

                foreach (var comment in comments)
                {
                    if (comment.EmbeddingData != null)
                    {
                        var entity = new ClusterizationEntity()
                        {
                            Comment = comment,
                            EmbeddingData = comment.EmbeddingData,
                            WorkspaceId = workspace.Id
                        };
                        workspace.Entities.Add(entity);
                        await entities_repository.AddAsync(entity);

                        continue;
                    }

                    var inputText = HttpUtility.UrlEncodeUnicode(comment.TextOriginal);
                    //var inputText = comment.TextOriginal.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("\"","\\\"").Replace("\\", "\\\\");

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
                                WorkspaceId = workspace.Id
                            };
                            workspace.Entities.Add(entity);
                            await entities_repository.AddAsync(entity);

                            percent += 100 / comments.Count();
                            await taskService.ChangeTaskPercent(taskId, percent);
                        }
                        else
                        {
                            throw new HttpException(localizer[ErrorMessagePatterns.EmbeddingsLoadingError], response.StatusCode);
                        }
                    }
                }

                workspace.IsAllDataEmbedded = true;
                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch{
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
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
