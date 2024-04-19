using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.EmbeddingModelDTOs.Responses;

namespace Domain.Interfaces.EmbeddingModels
{
    public interface IEmbeddingModelsService
    {
        public Task<ICollection<EmbeddingModelDTO>> GetAll();
        public Task<ICollection<ClusterizationDimensionTypeDTO>> GetModelDimensionTypes(string embeddingModelId);
    }
}
