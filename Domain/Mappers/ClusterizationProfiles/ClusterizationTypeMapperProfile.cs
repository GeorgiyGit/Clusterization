﻿using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    public class ClusterizationTypeMapperProfile : AutoMapper.Profile
    {
        public ClusterizationTypeMapperProfile()
        {
            CreateMap<ClusterizationType, ClusterizationTypeDTO>();
        }
    }
}
