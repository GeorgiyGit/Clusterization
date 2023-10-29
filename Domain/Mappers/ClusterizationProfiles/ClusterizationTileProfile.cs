using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    internal class ClusterizationTileProfile : AutoMapper.Profile
    {
        public ClusterizationTileProfile()
        {
            CreateMap<ClusterizationTile, ClusterizationTileDTO>()
                .ForMember(dest => dest.Points,
                           ost => ost.Ignore());
        }
    }
}
