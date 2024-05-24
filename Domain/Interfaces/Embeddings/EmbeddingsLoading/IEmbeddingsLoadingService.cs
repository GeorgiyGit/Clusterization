
namespace Domain.Interfaces.Embeddings.EmbeddingsLoading
{
    public interface IEmbeddingsLoadingService
    {
        public Task LoadEmbeddingsByProfile(int profileId);
        public Task LoadEmbeddingsByProfileBackgroundJob(int profileId, string userId, string groupTaskId, int subTasksPos);
        
        public Task LoadEmbeddingsByWorkspaceDataPack(int packId, string embeddingModelId);
    }
}
