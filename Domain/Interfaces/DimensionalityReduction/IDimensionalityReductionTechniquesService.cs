using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.DimensionalityReduction
{
    public interface IDimensionalityReductionTechniquesService
    {
        public Task<ICollection<DimensionalityReductionTechniqueDTO>> GetAll();
    }
}
