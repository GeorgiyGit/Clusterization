using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationColorValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        
        public ClusterizationPointColors? PointColors { get; set; }
        public int? PointColorsId { get; set; }

        public Cluster? Cluster { get; set; }
        public int? ClusterId { get; set; }
    }
}
