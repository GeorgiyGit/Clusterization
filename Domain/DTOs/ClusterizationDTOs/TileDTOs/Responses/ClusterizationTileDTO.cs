using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.TileDTOs
{
    public class ClusterizationTileDTO
    {
        public int Id { get; set; }

        public double Length { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public ICollection<DisplayedPointDTO> Points { get; set; } = new List<DisplayedPointDTO>();
    }
}
