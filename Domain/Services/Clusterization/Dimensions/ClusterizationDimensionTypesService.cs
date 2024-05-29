using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.Entities.Clusterization;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Interfaces.Clusterization.Dimensions;
using Domain.Interfaces.Other;
using Domain.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Dimensions
{
    public class ClusterizationDimensionTypesService : IClusterizationDimensionTypesService
    {
        private readonly IRepository<DimensionType> _repository;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository;

        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        public ClusterizationDimensionTypesService(IRepository<DimensionType> repository,
                                                   IRepository<EmbeddingModel> embeddingModelsRepository,
                                                   IMapper mapper,
                                                   IDistributedCache distributedCache)
        {
            _repository = repository;
            _embeddingModelsRepository = embeddingModelsRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<ICollection<ClusterizationDimensionTypeDTO>> GetAll()
        {
            var recordId = "ClusterizationDimensionTypes";

            var cache = await _distributedCache.GetRecordAsync<ICollection<ClusterizationDimensionTypeDTO>>(recordId);
            if (cache != null) return cache;

            var types = await _repository.GetAsync();
            var mappedTypes = _mapper.Map<ICollection<ClusterizationDimensionTypeDTO>>(types);

            await _distributedCache.SetRecordAsync<ICollection<ClusterizationDimensionTypeDTO>>(recordId, mappedTypes);

            return mappedTypes;
        }

        public async Task<ICollection<ClusterizationDimensionTypeDTO>> GetAllInEmbeddingModel(string embeddingModelId)
        {
            var embeddingModel = await _embeddingModelsRepository.FindAsync(embeddingModelId);

            var types = await _repository.GetAsync(e => e.DimensionCount <= embeddingModel.DimensionTypeId);

            return _mapper.Map<ICollection<ClusterizationDimensionTypeDTO>>(types);
        }
    }
}
