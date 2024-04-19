using Accord.MachineLearning.Clustering;
using Accord.Statistics.Analysis;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Embeddings.EmbeddingsLoading;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DimensionalityReduction
{
    public class DimensionalityReductionService:IDimensionalityReductionService
    {
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<DimensionEmbeddingObject> _dimensionEmbeddingObjectsRepository;
        private readonly IRepository<EmbeddingObjectsGroup> _embeddingObjectsGroupsRepository;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IEmbeddingsService _embeddingsService;
        public DimensionalityReductionService(IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<MyDataObject> dataObjectsRepository,
            IRepository<DimensionEmbeddingObject> dimensionEmbeddingObjectsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IEmbeddingsService embeddingsService,
            IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository,
            IRepository<EmbeddingModel> embeddingModelsRepository)
        {
            _workspacesRepository = workspacesRepository;
            _dataObjectsRepository = dataObjectsRepository;
            _dimensionEmbeddingObjectsRepository = dimensionEmbeddingObjectsRepository;
            _localizer = localizer;
            _embeddingsService = embeddingsService;
            _embeddingObjectsGroupsRepository = embeddingObjectsGroupsRepository;
            _embeddingModelsRepository = embeddingModelsRepository;
        }

        public async Task AddEmbeddingValues(int workspaceId, string DRTechniqueId, string embeddingModelId, int dimensionCount)
        {
            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            var helpModels = new List<AddEmbeddingsWithDRHelpModel>(workspace.EntitiesCount);

            bool isNotAll2Flag = false;
            bool isNotAllCurrentFlag = false;

            var embeddingModel = (await _embeddingModelsRepository.GetAsync(e => e.Id == embeddingModelId, includeProperties: $"{nameof(EmbeddingModel.DimensionType)}")).FirstOrDefault();

            foreach(var dataObject in workspace.DataObjects)
            {
                var originalDataObject = (await _dataObjectsRepository.GetAsync(e => e.Id == dataObject.Id)).FirstOrDefault();

                var embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DRTechniqueId == DRTechniqueId && e.EmbeddingModelId == embeddingModelId && e.WorkspaceId == workspaceId && e.DataObjectId == dataObject.Id,includeProperties:$"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();


                var originalEmbeddingGroup = originalDataObject.EmbeddingObjectsGroups.Where(e => e.DRTechniqueId == DimensionalityReductionTechniques.Original && e.EmbeddingModelId == embeddingModelId).FirstOrDefault();

                if (originalEmbeddingGroup == null) throw new HttpException(_localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);

                var dimensionEmbedding = (await _dimensionEmbeddingObjectsRepository.GetAsync(e => e.EmbeddingObjectsGroupId == originalEmbeddingGroup.Id && e.TypeId== embeddingModel.DimensionTypeId)).FirstOrDefault();

                if (dimensionEmbedding == null) throw new HttpException(_localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);

                var embedding = dimensionEmbedding.ValuesString.Split(' ').Select(double.Parse).ToArray();

                helpModels.Add(new AddEmbeddingsWithDRHelpModel()
                {
                    DataObject = originalDataObject,
                    DataPoints = embedding
                });


                if (embeddingObjectsGroup == null)
                {
                    isNotAll2Flag = true;

                    if (dimensionCount != embeddingModel.DimensionTypeId)
                    {
                        isNotAllCurrentFlag = true;
                    }

                    await _embeddingsService.AddEmbeddingsToDataObject(embedding.ToList(), workspaceId, DRTechniqueId, embeddingModelId, originalDataObject.Id, embeddingModel.DimensionTypeId);
                }
                else if (!isNotAll2Flag && !isNotAllCurrentFlag)
                {
                    var dimensionEmbeddingCurrent = embeddingObjectsGroup.DimensionEmbeddingObjects.Where(e => e.TypeId == dimensionCount);

                    if (dimensionEmbeddingCurrent == null)
                    {
                        isNotAllCurrentFlag = false;
                    }

                    var dimensionEmbedding2 = embeddingObjectsGroup.DimensionEmbeddingObjects.Where(e => e.TypeId == 2);

                    if (dimensionEmbedding2 == null)
                    {
                        isNotAll2Flag = false;
                    }
                }
            }

            if (isNotAllCurrentFlag && embeddingModel.DimensionTypeId != dimensionCount)
            {
                await ProjectData(helpModels, DRTechniqueId, dimensionCount, DRTechniqueId, workspaceId, embeddingModelId);
            }
            if (isNotAll2Flag)
            {
                await ProjectData(helpModels, DRTechniqueId, 2, DRTechniqueId, workspaceId, embeddingModelId);
            }
        }
        public async Task ProjectData(List<AddEmbeddingsWithDRHelpModel> helpModels, string DRTechniqueId, int numberOfDimensions, string clusterizationWorkspaceDRTechnique, int workspaceId, string embeddingModelId)
        {
            var values = helpModels.Select(e => e.DataPoints).ToArray();
            double[][] reducedDimensionality = new double[1][];
            if (DRTechniqueId == DimensionalityReductionTechniques.PCA)
            {
                var pca = new PrincipalComponentAnalysis()
                {
                    Method = PrincipalComponentMethod.Center,
                    Whiten = true
                };

                pca.Learn(values);

                reducedDimensionality = pca.Transform(values, numberOfDimensions);
            }
            else if (DRTechniqueId == DimensionalityReductionTechniques.t_SNE)
            {
                var tsne = new TSNE()
                {
                    NumberOfOutputs = numberOfDimensions, // The number of dimensions for the output
                };

                tsne.Perplexity = Math.Floor((values.Length - 2) / 3.0);

                reducedDimensionality = tsne.Transform(values);
            }
            else if (DRTechniqueId == DimensionalityReductionTechniques.LLE)
            {
                // Perform LLE
                Matrix<double> matrix = Matrix<double>.Build.Dense(values.Length, values[0].Length);

                for (int i = 0; i < values.Length; i++)
                {
                    for (int j = 0; j < values[0].Length; j++)
                    {
                        matrix[i, j] = values[i][j];
                    }
                }

                //Matrix<double> resMatrix = LLE(matrix, numNeighbors, numberOfDimensions);

                reducedDimensionality = new double[values.Length][];
                for (int i = 0; i < values.Length; i++)
                {
                    reducedDimensionality[i] = new double[values[0].Length];
                    for (int j = 0; j < values[0].Length; j++)
                    {
                        //reducedDimensionality[i][j] = resMatrix[i, j];
                    }
                }
            }
            else
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.DRTechniqueNotFound], HttpStatusCode.NotFound);
            }

            for (int i = 0; i < helpModels.Count; i++)
            {
                await _embeddingsService.AddEmbeddingsToDataObject(reducedDimensionality[i].ToList(), workspaceId, DRTechniqueId, embeddingModelId, helpModels[i].DataObject.Id, numberOfDimensions);
            }
        }
    }
}
