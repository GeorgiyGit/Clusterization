using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.Entities.Quotas;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace Domain.Services.Quotas
{
    public class QuotasTypesService : IQuotasTypesService
    {
        private readonly IRepository<QuotasType> _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        public QuotasTypesService(IRepository<QuotasType> repository,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<ICollection<QuotasTypeDTO>> GetAllTypes()
        {
            var recordId = "QuotasTypes";

            var cache = await _distributedCache.GetRecordAsync<ICollection<QuotasTypeDTO>>(recordId);
            if (cache != null) return cache;

            var types = await _repository.GetAsync();
            var mappedTypes = _mapper.Map<ICollection<QuotasTypeDTO>>(types);

            await _distributedCache.SetRecordAsync<ICollection<QuotasTypeDTO>>(recordId, mappedTypes);

            return mappedTypes;
        }
    }
}
