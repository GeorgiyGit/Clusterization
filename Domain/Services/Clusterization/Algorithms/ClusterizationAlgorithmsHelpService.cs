using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Algorithms
{
    public class ClusterizationAlgorithmsHelpService: IClusterizationAlgorithmsHelpService
    {
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<DimensionalityReductionValue> drValues_repository;
        private readonly IRepository<ClusterizationWorkspaceDRTechnique> workspaceDRTechniques_repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;

        public ClusterizationAlgorithmsHelpService(IRepository<ClusterizationEntity> entities_repository,
                                                   IRepository<DimensionalityReductionValue> drValues_repository,
                                                   IRepository<ClusterizationWorkspaceDRTechnique> workspaceDRTechniques_repository,
                                                   IStringLocalizer<ErrorMessages> localizer)
        {
            this.entities_repository = entities_repository;
            this.drValues_repository = drValues_repository;
            this.workspaceDRTechniques_repository = workspaceDRTechniques_repository;
            this.localizer = localizer;
        }
        public async Task<List<AddEmbeddingsWithDRHelpModel>> CreateHelpModels(ClusterizationProfile profile)
        {
            List<AddEmbeddingsWithDRHelpModel> helpModels = new List<AddEmbeddingsWithDRHelpModel>();

            string drTechniqueId = profile.DimensionalityReductionTechniqueId;
            int dimensionsCount = profile.DimensionCount;

            if (drTechniqueId == DimensionalityReductionTechniques.JSL || dimensionsCount == 1536)
            {
                var clusterizationEntities = (await entities_repository.GetAsync(e => e.WorkspaceId == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationEntity.EmbeddingData)}")).ToList();
                foreach (var entity in clusterizationEntities)
                {
                    DimensionalityReductionValue drValue = (await drValues_repository.GetAsync(e => e.TechniqueId == DimensionalityReductionTechniques.JSL && e.EmbeddingDataId == entity.EmbeddingDataId, includeProperties: $"{nameof(DimensionalityReductionValue.Embeddings)}")).FirstOrDefault();

                    if (drValue == null) throw new HttpException(localizer[ErrorMessagePatterns.DRValueNotFound], HttpStatusCode.NotFound);

                    var dimensionValue = drValue.Embeddings.First(e => e.DimensionTypeId == dimensionsCount);

                    double[] embeddingValues = dimensionValue.ValuesString.Split(' ').Select(double.Parse).ToArray();

                    helpModels.Add(new AddEmbeddingsWithDRHelpModel()
                    {
                        Entity = entity,
                        DataPoints = embeddingValues
                    });
                }
                return helpModels;
            }
            else
            {
                var workspaceDRTechnique = (await workspaceDRTechniques_repository.GetAsync(e => e.DRTechnique.Id == drTechniqueId && e.WorkspaceId == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspaceDRTechnique.DRValues)}")).FirstOrDefault();

                if (workspaceDRTechnique == null) throw new HttpException(localizer[ErrorMessagePatterns.DRValueNotFound], HttpStatusCode.NotFound);

                var drValues = (await drValues_repository.GetAsync(e => e.TechniqueId == drTechniqueId && e.ClusterizationWorkspaceDRTechniqueId == workspaceDRTechnique.Id));

                foreach (var drValue in drValues)
                {
                    var dimensionValue = drValue.Embeddings.First(e => e.DimensionTypeId == dimensionsCount);

                    double[] embeddingValues = dimensionValue.ValuesString.Split(' ').Select(double.Parse).ToArray();

                    helpModels.Add(new AddEmbeddingsWithDRHelpModel()
                    {
                        Entity = drValue.ClusterizationEntity,
                        DataPoints = embeddingValues
                    });
                }
                return helpModels;
            }
        }
    }
}
