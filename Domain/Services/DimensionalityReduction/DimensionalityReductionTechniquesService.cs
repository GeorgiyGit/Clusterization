using AutoMapper;
using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.Entities.DimensionalityReduction;
using Domain.Interfaces;
using Domain.Interfaces.DimensionalityReduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DimensionalityReduction
{
    public class DimensionalityReductionTechniquesService : IDimensionalityReductionTechniquesService
    {
        private readonly IRepository<DimensionalityReductionTechnique> drTechnique_repository;
        private readonly IMapper mapper;
        public DimensionalityReductionTechniquesService(IRepository<DimensionalityReductionTechnique> drTechnique_repository,
                                                        IMapper mapper)
        {
            this.drTechnique_repository = drTechnique_repository;
            this.mapper = mapper;
        }

        public async Task<ICollection<DimensionalityReductionTechniqueDTO>> GetAll()
        {
            var techniques = await drTechnique_repository.GetAsync();

            return mapper.Map<ICollection<DimensionalityReductionTechniqueDTO>>(techniques);
        }
    }
}
