using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.Entities.Clusterization.Displaying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles.Displaying
{
    internal class DisplayedPointProfile : AutoMapper.Profile
    {
        public DisplayedPointProfile()
        {
            CreateMap<DisplayedPoint, DisplayedPointDTO>()
                    .ForMember(dest => dest.Color,
                               ost => ost.MapFrom(e => e.Cluster.Color));
        }
    }
}
