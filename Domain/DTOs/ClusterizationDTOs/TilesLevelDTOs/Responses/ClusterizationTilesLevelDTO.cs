using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs
{
    public class ClusterizationTilesLevelDTO
    {
        public int Id { get; set; }
        public int X { get; set; }
        public double TileLength { get; set; }
        public int TileCount { get; set; }

        public double MinXValue { get; set; }
        public double MinYValue { get; set; }

        public double MaxXValue { get; set; }
        public double MaxYValue { get; set; }
    }
}
