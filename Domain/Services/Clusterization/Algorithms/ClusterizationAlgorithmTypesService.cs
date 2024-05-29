using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Other;
using Domain.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Services.Clusterization.Algorithms
{
    public class ClusterizationAlgorithmTypesService : IClusterizationAlgorithmTypesService
    {
        private readonly IRepository<ClusterizationAlgorithmType> _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        public ClusterizationAlgorithmTypesService(IRepository<ClusterizationAlgorithmType> repository,
                                                   IMapper mapper,
                                                   IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }
        public async Task<ICollection<SimpleAlgorithmTypeDTO>> GetAll()
        {
            var recordId = "AlgorithmTypes";

            var cache = await _distributedCache.GetRecordAsync<ICollection<SimpleAlgorithmTypeDTO>>(recordId);
            if (cache != null) return cache;

            var types = await _repository.GetAsync();
            var mappedTypes = _mapper.Map<ICollection<SimpleAlgorithmTypeDTO>>(types);

            await _distributedCache.SetRecordAsync<ICollection<SimpleAlgorithmTypeDTO>>(recordId, mappedTypes);
            return mappedTypes;
        }
    }
}
