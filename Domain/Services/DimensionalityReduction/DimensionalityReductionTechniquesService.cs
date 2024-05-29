using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.Entities.DimensionalityReductionEntities;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Other;
using Domain.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace Domain.Services.DimensionalityReduction
{
    public class DimensionalityReductionTechniquesService : IDimensionalityReductionTechniquesService
    {
        private readonly IRepository<DimensionalityReductionTechnique> _drTechniquesRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        public DimensionalityReductionTechniquesService(IRepository<DimensionalityReductionTechnique> drTechnique_repository,
                                                        IMapper mapper,
                                                        IDistributedCache distributedCache)
        {
            _drTechniquesRepository = drTechnique_repository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<ICollection<DimensionalityReductionTechniqueDTO>> GetAll()
        {
            var recordId = "DimensionalityReductionTechniques";

            var cache = await _distributedCache.GetRecordAsync<ICollection<DimensionalityReductionTechniqueDTO>>(recordId);
            if (cache != null) return cache;

            var techniques = await _drTechniquesRepository.GetAsync();
            var mappedTechniques = _mapper.Map<ICollection<DimensionalityReductionTechniqueDTO>>(techniques);

            await _distributedCache.SetRecordAsync<ICollection<DimensionalityReductionTechniqueDTO>>(recordId, mappedTechniques);

            return mappedTechniques;
        }
    }
}
