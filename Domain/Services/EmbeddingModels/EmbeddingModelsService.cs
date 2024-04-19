using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.EmbeddingModelDTOs.Responses;
using Domain.Entities.EmbeddingModels;
using Domain.Interfaces.EmbeddingModels;
using Domain.Interfaces.Other;
using Domain.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.EmbeddingModels
{
    public class EmbeddingModelsService : IEmbeddingModelsService
    {
        private readonly IRepository<EmbeddingModel> _repository;

        private readonly IMapper _mapper;
        public EmbeddingModelsService(IRepository<EmbeddingModel> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<EmbeddingModelDTO>> GetAll()
        {
            var models = await _repository.GetAsync(includeProperties: $"{nameof(EmbeddingModel.DimensionType)}");

            return _mapper.Map<ICollection<EmbeddingModelDTO>>(models);
        }

        public Task<ICollection<ClusterizationDimensionTypeDTO>> GetModelDimensionTypes(string embeddingModelId)
        {
            throw new NotImplementedException();
        }
    }
}
