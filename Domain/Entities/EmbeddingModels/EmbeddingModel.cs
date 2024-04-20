using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Embeddings;
using Domain.Entities.Embeddings.DimensionEntities;

namespace Domain.Entities.EmbeddingModels
{
    public class EmbeddingModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int MaxInputCount { get; set; }
        
        public int QuotasPrice { get; set; }

        public DimensionType DimensionType { get; set; }
        public int DimensionTypeId { get; set; }

        public ICollection<EmbeddingObjectsGroup> Groups { get; set; } = new HashSet<EmbeddingObjectsGroup>();

        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();

        public ICollection<EmbeddingLoadingState> EmbeddingLoadingStates { get; set; } = new HashSet<EmbeddingLoadingState>();
    }
}
