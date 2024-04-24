using AutoMapper;
using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.Entities.DimensionalityReductionEntities;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Other;

namespace Domain.Services.DimensionalityReduction
{
    public class DimensionalityReductionTechniquesService : IDimensionalityReductionTechniquesService
    {
        private readonly IRepository<DimensionalityReductionTechnique> _drTechniquesRepository;
        private readonly IMapper _mapper;
        public DimensionalityReductionTechniquesService(IRepository<DimensionalityReductionTechnique> drTechnique_repository,
                                                        IMapper mapper)
        {
            _drTechniquesRepository = drTechnique_repository;
            _mapper = mapper;
        }

        public async Task<ICollection<DimensionalityReductionTechniqueDTO>> GetAll()
        {
            var techniques = await _drTechniquesRepository.GetAsync();

            return _mapper.Map<ICollection<DimensionalityReductionTechniqueDTO>>(techniques);
        }
    }
}
