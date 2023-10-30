﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationTilesLevel
    {
        public int Id { get; set; }
        
        public int Z { get; set; }
        public double TileLength { get; set; }
        public int TileCount { get; set; }

        public double MinXValue { get; set; }
        public double MinYValue { get; set; }

        public double MaxXValue { get; set; }
        public double MaxYValue { get; set; }


        public ICollection<ClusterizationTile> Tiles { get; set; } = new HashSet<ClusterizationTile>();

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
