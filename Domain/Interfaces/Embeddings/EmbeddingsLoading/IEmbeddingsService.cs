
namespace Domain.Interfaces.Embeddings.EmbeddingsLoading
{
    public interface IEmbeddingsService
    {
        public Task AddEmbeddingsToDataObject(List<double> embedding, int workspaceId, string DRTechniqueId, string embeddingModelId, long dataObjectId, int dimensionsCount);
    }
}
