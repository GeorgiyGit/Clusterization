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
        public double Length { get; set; }
        public int TileCount { get; set; }
    }
}
