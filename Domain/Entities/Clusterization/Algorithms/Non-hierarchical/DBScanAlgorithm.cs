using Domain.Entities.Clusterization.Algorithms;

namespace Domain.Entitie.Clusterization.Algorithms.Non_hierarchical
{
    public class DBSCANAlgorithm: ClusterizationAbstactAlgorithm
    {
        public double Epsilon { get; set; }
        public int MinimumPointsPerCluster { get; set; }
    }
}
