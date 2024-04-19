
namespace Domain.Entities.Clusterization.Algorithms.Non_hierarchical
{
    public class KMeansAlgorithm : ClusterizationAbstactAlgorithm
    {
        public int NumClusters { get; set; }
        public int Seed { get; set; }
    }
}
