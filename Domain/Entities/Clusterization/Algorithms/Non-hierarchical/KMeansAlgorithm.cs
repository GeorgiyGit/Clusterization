using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization.Algorithms.Non_hierarchical
{
    public class KMeansAlgorithm : ClusterizationAbstactAlgorithm
    {
        public int NumClusters { get; set; }
        public int Seed { get; set; }
    }
}
