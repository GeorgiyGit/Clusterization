using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs
{
    public class DisplayedPointDTO
    {
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public string Color { get; set; }
    }
}
