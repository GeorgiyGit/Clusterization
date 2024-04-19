
namespace Domain.Entities.Clusterization.Algorithms.Non_hierarchical
{
    public class SpectralClusteringAlgorithm : ClusterizationAbstactAlgorithm
    {
        public int NumClusters { get; set; }
        public double Gamma { get; set; }
    }
}
