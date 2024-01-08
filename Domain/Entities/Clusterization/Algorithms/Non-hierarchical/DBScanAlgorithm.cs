using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization.Algorithms.Non_hierarchical
{
    public class DBScanAlgorithm: ClusterizationAbstactAlgorithm
    {
        public double Epsilon { get; set; }
        public int MinimumPointsPerCluster { get; set; }
    }
}
