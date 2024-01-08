using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization.Algorithms.Non_hierarchical
{
    public class GaussianMixtureAlgorithm : ClusterizationAbstactAlgorithm
    {
        public int NumberOfComponents { get; set; }
    }
}
