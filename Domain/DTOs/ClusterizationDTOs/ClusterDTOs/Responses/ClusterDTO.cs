﻿using Domain.Entities.Clusterization.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Responses
{
    public class ClusterDTO
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int? ParentClusterId { get; set; }
        public string? Name { get; set; }
        public int ChildElementsCount { get; set; }
    }
}
