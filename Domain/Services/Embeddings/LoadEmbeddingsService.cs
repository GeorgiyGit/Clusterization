using Domain.Entities.Clusterization;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Embeddings;
using Domain.LoadHelpModels;
using Domain.Resources.Localization.Errors;
using Hangfire;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Embeddings
{
    public class LoadEmbeddingsService : ILoadEmbeddingsService
    {
        private readonly IRepository<Comment> comment_repository;
        private readonly IRepository<ClusterizationWorkspace> workspace_repository;
        private readonly IClusterizationDimensionTypeService DimensionTypeService;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IEmbeddingsService embeddingsService;
        private readonly IBackgroundJobClient backgroundJobClient;

        private readonly string apiKey;

        public LoadEmbeddingsService(IRepository<Comment> comment_repository,
                                     IClusterizationDimensionTypeService DimensionTypeService,
                                     IRepository<ClusterizationWorkspace> workspace_repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IEmbeddingsService embeddingsService,
                                     IBackgroundJobClient backgroundJobClient)
        {
            this.comment_repository = comment_repository;
            this.DimensionTypeService = DimensionTypeService;
            this.workspace_repository = workspace_repository;
            this.localizer = localizer;
            this.backgroundJobClient = backgroundJobClient;


            var openAIOptions = configuration.GetSection("OpenAIOptions");

            this.apiKey = openAIOptions["ApiKey"];
            this.embeddingsService = embeddingsService;
        }
        public async Task LoadEmbeddingsByWorkspace(int workspaceId)
        {
            backgroundJobClient.Enqueue(() => LoadEmbeddingsBackProcess(workspaceId));
        }
        private async Task LoadEmbeddingsBackProcess(int workspaceId)
        {
            var workspace = (await workspace_repository.GetAsync(c => c.Id == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            if (workspace.IsAllDataEmbedded) return;


            string model = "text-embedding-ada-002";
            string apiUrl = "https://api.openai.com/v1/embeddings";

            var Dimensions = (await DimensionTypeService.GetAll()).ToList();

            var comments = await comment_repository.GetAsync(c => c.Workspaces.Contains(workspace), includeProperties: $"{nameof(Comment.Workspaces)},{nameof(Comment.EmbeddingData)}");
            foreach (var comment in comments)
            {
                if (comment.EmbeddingData != null) continue;

                var inputText = comment.TextOriginal;

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


                        for(int i = 0; i < Dimensions.Count; i++)
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
                    }
                    else
                    {
                        throw new HttpException(localizer[ErrorMessagePatterns.EmbeddingsLoadingError], response.StatusCode);
                    }
                }
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
