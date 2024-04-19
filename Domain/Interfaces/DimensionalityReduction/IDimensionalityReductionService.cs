
namespace Domain.Interfaces.DimensionalityReduction
{
    public interface IDimensionalityReductionService
    {
        public Task AddEmbeddingValues(int workspaceId, string DRTechniqueId, string embeddingModelId, int dimensionCount);
    }
}
