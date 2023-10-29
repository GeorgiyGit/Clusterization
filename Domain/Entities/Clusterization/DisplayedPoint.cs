using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class DisplayedPoint
    {
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public int OptimizationLevel { get; set; }

        public string ValueId { get; set; }

        public ClusterizationTile Tile { get; set; }
        public int TileId { get; set; }

        public Cluster Cluster { get; set; }
        public int ClusterId { get; set; }
    }
}
