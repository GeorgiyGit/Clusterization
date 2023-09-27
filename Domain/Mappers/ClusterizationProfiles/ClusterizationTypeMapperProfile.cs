using Domain.DTOs.ClusterizationDTOs.ModelDTOs;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    internal class ClusterizationTypeMapperProfile : AutoMapper.Profile
    {
        public ClusterizationTypeMapperProfile()
        {
            CreateMap<ClusterizationType, ClusterizationTypeDTO>();
        }
    }
}
