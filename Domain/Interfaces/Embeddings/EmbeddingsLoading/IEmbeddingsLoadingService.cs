
namespace Domain.Interfaces.Embeddings.EmbeddingsLoading
{
    public interface IEmbeddingsLoadingService
    {
        public Task LoadEmbeddingsByProfile(int profileId);
        public Task LoadEmbeddingsByWorkspaceDataPack(int packId, string embeddingModelId);
    }
}
