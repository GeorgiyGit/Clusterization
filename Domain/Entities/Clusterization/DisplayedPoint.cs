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

        public ICollection<ClusterizationPointColors> Colors { get; set; } = new HashSet<ClusterizationPointColors>();

        public ICollection<DisplayedPoint> Points { get; set; } = new HashSet<DisplayedPoint>();
        
        public DisplayedPoint? ParentPoint { get; set; }
        public int? ParentPointId { get; set; }

        public ClusterizationEntity? ClusterizationEntity { get; set; }
        public int? ClusterizationEntityId { get; set; }
    }
}
