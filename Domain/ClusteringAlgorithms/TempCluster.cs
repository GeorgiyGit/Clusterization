using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClusteringAlgorithms
{
    public class TempCluster
    {
        public int Id { get; set; }
        public List<int> EntityIds { get; set; } = new List<int>();
    }
}
