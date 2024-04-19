using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.EmbeddingModels;

namespace Domain.Entities.Embeddings
{
    public class EmbeddingLoadingState
    {
        public int Id { get; set; }

        public bool IsAllEmbeddingsLoaded { get; set; }
        
        public EmbeddingModel EmbeddingModel { get; set; }
        public string EmbeddingModelId { get; set; }

        public ClusterizationProfile? Profile { get; set; }
        public int? ProfileId { get; set; }
        
        public WorkspaceDataObjectsAddPack? AddPack { get; set; }
        public int? AddPackId { get; set; }
    }
}
