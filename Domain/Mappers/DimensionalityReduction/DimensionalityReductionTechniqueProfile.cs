using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.Entities.DimensionalityReductionEntities;

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
