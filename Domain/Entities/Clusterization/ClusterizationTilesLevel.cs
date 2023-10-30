using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationTilesLevel
    {
        public int Id { get; set; }
        
        public int X { get; set; }
        public double TileLength { get; set; }
        public int TileCount { get; set; }

        public ICollection<ClusterizationTile> Tiles { get; set; } = new HashSet<ClusterizationTile>();

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
