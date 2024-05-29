using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.EmbeddingModelDTOs.Responses;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Exceptions;
using Domain.Interfaces.EmbeddingModels;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Domain.Services.EmbeddingModels
{
    public class EmbeddingModelsService : IEmbeddingModelsService
    {
        private readonly IRepository<EmbeddingModel> _repository;
        private readonly IRepository<DimensionType> _dimensionTypesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public EmbeddingModelsService(IRepository<EmbeddingModel> repository,
            IMapper mapper,
            IRepository<DimensionType> dimensionTypesRepositor,
            IStringLocalizer<ErrorMessages> localizer,
            IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _dimensionTypesRepository = dimensionTypesRepositor;
            _localizer = localizer;
            _distributedCache = distributedCache;
        }
        public async Task<ICollection<EmbeddingModelDTO>> GetAll()
        {
            var recordId = "EmbeddingModels";

            var cache = await _distributedCache.GetRecordAsync<ICollection<EmbeddingModelDTO>>(recordId);
            if (cache != null) return cache;

            var models = await _repository.GetAsync(includeProperties: $"{nameof(EmbeddingModel.DimensionType)}");
            var mappedModels = _mapper.Map<ICollection<EmbeddingModelDTO>>(models);

            await _distributedCache.SetRecordAsync<ICollection<EmbeddingModelDTO>>(recordId, mappedModels);

            return mappedModels;
        }

        public async Task<ICollection<ClusterizationDimensionTypeDTO>> GetModelDimensionTypes(string embeddingModelId)
        {
            var model = await _repository.FindAsync(embeddingModelId);
            if (model == null) throw new HttpException(_localizer[ErrorMessages.EmbeddingModelNotFound], HttpStatusCode.NotFound);

            var dimensionTypes = await _dimensionTypesRepository.GetAsync(e => e.DimensionCount <= model.DimensionTypeId);

            return _mapper.Map<ICollection<ClusterizationDimensionTypeDTO>>(dimensionTypes);
        }
    }
}
