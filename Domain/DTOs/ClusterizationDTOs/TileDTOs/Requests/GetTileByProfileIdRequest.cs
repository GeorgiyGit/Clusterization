﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.TileDTOs
{
    public class GetTileByProfileIdRequest
    {
        public int ProfileId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public ICollection<int> AllowedClusterIds { get; set; } = new List<int>();
    }
}
