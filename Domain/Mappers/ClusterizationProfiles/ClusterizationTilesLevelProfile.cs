using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Entities.Clusterization.Displaying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    internal class ClusterizationTilesLevelProfile : AutoMapper.Profile
    {
        public ClusterizationTilesLevelProfile()
        {
            CreateMap<ClusterizationTilesLevel, ClusterizationTilesLevelDTO>();
        }
    }
}
