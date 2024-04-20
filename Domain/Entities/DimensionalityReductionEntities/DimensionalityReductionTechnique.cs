using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Embeddings;

namespace Domain.Entities.DimensionalityReductionEntities
{
    public class DimensionalityReductionTechnique
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmbeddingObjectsGroup> Groups { get; set; } = new HashSet<EmbeddingObjectsGroup>();
        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();
    }
}
