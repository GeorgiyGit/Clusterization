using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;

namespace Domain.Interfaces.DimensionalityReduction
{
    public interface IDimensionalityReductionTechniquesService
    {
        public Task<ICollection<DimensionalityReductionTechniqueDTO>> GetAll();
    }
}
