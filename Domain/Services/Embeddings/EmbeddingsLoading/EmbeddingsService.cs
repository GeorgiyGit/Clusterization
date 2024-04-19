using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Entities.Embeddings;
using Domain.Interfaces.Embeddings.EmbeddingsLoading;
using Domain.Interfaces.Other;

namespace Domain.Services.Embeddings.EmbeddingsLoading
{
    public class EmbeddingsService:IEmbeddingsService
    {
        private readonly IRepository<EmbeddingObjectsGroup> _embeddingObjectsGroupsRepository;
        private readonly IRepository<DimensionEmbeddingObject> _dimensionEmbeddingsRepository;

        public EmbeddingsService(IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository,
            IRepository<DimensionEmbeddingObject> dimensionEmbeddingsRepository)
        {
            _embeddingObjectsGroupsRepository = embeddingObjectsGroupsRepository;
            _dimensionEmbeddingsRepository = dimensionEmbeddingsRepository;
        }

        public async Task AddEmbeddingsToDataObject(List<double> embedding, int workspaceId, string DRTechniqueId, string embeddingModelId, long dataObjectId, int dimensionsCount)
        {
            var embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DataObjectId == dataObjectId && e.DRTechniqueId == DRTechniqueId && e.EmbeddingModelId == embeddingModelId && e.WorkspaceId == workspaceId, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();

            if (embeddingObjectsGroup == null)
            {
                var newEOG = new EmbeddingObjectsGroup()
                {
                    DataObjectId = dataObjectId,
                    DRTechniqueId = DRTechniqueId,
                    EmbeddingModelId = embeddingModelId,
                    WorkspaceId = workspaceId
                };

                await _embeddingObjectsGroupsRepository.AddAsync(newEOG);
                await _embeddingObjectsGroupsRepository.SaveChangesAsync();

                embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DataObjectId == dataObjectId && e.DRTechniqueId == DRTechniqueId && e.EmbeddingModelId == embeddingModelId && e.WorkspaceId == workspaceId, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();
            }

            if (!embeddingObjectsGroup.DimensionEmbeddingObjects.Any(e => e.TypeId == dimensionsCount))
            {
                string valuesString = "";
                foreach (var embeddingValue in embedding)
                {
                    valuesString += embeddingValue + " ";
                }
                valuesString = valuesString.TrimEnd(' ');

                var dimensionEmbedding = new DimensionEmbeddingObject()
                {
                    EmbeddingObjectsGroupId = embeddingObjectsGroup.Id,
                    TypeId = dimensionsCount,
                    ValuesString = valuesString
                };

                await _dimensionEmbeddingsRepository.AddAsync(dimensionEmbedding);
                await _dimensionEmbeddingsRepository.SaveChangesAsync();
            }
        }
    }
}
