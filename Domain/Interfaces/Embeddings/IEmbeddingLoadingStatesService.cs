namespace Domain.Interfaces.Embeddings
{
    public interface IEmbeddingLoadingStatesService
    {
        public Task AddStatesToPack(int packId);
        public Task ReviewStates(int workspaceId);
    }
}
