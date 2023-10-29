using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationTile
    {
        public int Id { get; set; }

        public double Length { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public ICollection<ClusterizationTile> ChildTiles { get; set; } = new HashSet<ClusterizationTile>();
        
        public ClusterizationTile? Parent { get; set; }
        public int? ParentId { get; set; }

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }

        public ICollection<DisplayedPoint> Points { get; set; } = new HashSet<DisplayedPoint>();
    }
}
