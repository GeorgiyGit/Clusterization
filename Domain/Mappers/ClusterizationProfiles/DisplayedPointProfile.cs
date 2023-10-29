using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    internal class DisplayedPointProfile : AutoMapper.Profile
    {
        public DisplayedPointProfile()
        {
            CreateMap<DisplayedPoint, DisplayedPointDTO>();
        }
    }
}
