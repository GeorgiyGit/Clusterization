using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
