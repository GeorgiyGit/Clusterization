using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Other;
using Domain.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace Domain.Services.Clusterization
{
    public class ClusterizationTypeService : IClusterizationTypesService
    {
        private readonly IRepository<ClusterizationType> _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public ClusterizationTypeService(IRepository<ClusterizationType> repository,
                                         IMapper mapper,
                                         IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }
        public async Task<ICollection<ClusterizationTypeDTO>> GetAll()
        {
            var recordId = "ClusterizationTypes";

            var cache = await _distributedCache.GetRecordAsync<ICollection<ClusterizationTypeDTO>>(recordId);
            if (cache != null) return cache;

            var types = await _repository.GetAsync();
            var mappedTypes = _mapper.Map<ICollection<ClusterizationTypeDTO>>(types);

            await _distributedCache.SetRecordAsync<ICollection<ClusterizationTypeDTO>>(recordId, mappedTypes);

            return mappedTypes;
        }
    }
}
