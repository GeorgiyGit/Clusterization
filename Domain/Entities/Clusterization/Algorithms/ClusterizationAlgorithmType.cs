
namespace Domain.Entities.Clusterization.Algorithms
{
    public class ClusterizationAlgorithmType
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ClusterizationAbstactAlgorithm> ClusterizationAlgorithms { get; set; } = new HashSet<ClusterizationAbstactAlgorithm>();
    }
}
