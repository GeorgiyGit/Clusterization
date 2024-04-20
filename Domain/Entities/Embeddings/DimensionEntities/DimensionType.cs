using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.EmbeddingModels;

namespace Domain.Entities.Embeddings.DimensionEntities
{
    public class DimensionType
    {
        public int DimensionCount { get; set; }

        public ICollection<EmbeddingModel> EmbeddingModels { get; set; } = new HashSet<EmbeddingModel>();
        public ICollection<DimensionEmbeddingObject> DimensionEmbeddingObjects { get; set; } = new HashSet<DimensionEmbeddingObject>();
        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();
    }
}
