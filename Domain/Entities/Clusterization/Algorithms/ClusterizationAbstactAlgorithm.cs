
using Domain.Entities.Clusterization.Profiles;

namespace Domain.Entities.Clusterization.Algorithms
{
    public class ClusterizationAbstactAlgorithm
    {
        public int Id { get; set; }

        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();

        public ClusterizationAlgorithmType Type { get; set; }
        public string TypeId { get; set; }
    }
}
