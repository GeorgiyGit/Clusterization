﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs
{
    public class CustomerGetClusterizationProfilesRequest
    {
        public PageParameters PageParameters { get; set; }

        public string? AlgorithmTypeId { get; set; }
        public int? DimensionCount { get; set; }

        public string? EmbeddingModelId { get; set; }
    }
}
