using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.Entities.DimensionalityReduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DimensionalityReduction
{
    internal class DimensionalityReductionTechniqueProfile : AutoMapper.Profile
    {
        public DimensionalityReductionTechniqueProfile()
        {
            CreateMap<DimensionalityReductionTechnique, DimensionalityReductionTechniqueDTO>();
        }
    }
}
