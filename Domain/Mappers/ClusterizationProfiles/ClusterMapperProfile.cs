using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Responses;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    public class ClusterMapperProfile:AutoMapper.Profile
    {
        public ClusterMapperProfile()
        {
            CreateMap<Cluster, ClusterDTO>();
        }
    }
}
