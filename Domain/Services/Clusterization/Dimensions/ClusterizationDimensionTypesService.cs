using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.Entities.Clusterization;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Interfaces.Clusterization.Dimensions;
using Domain.Interfaces.Other;
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
        public ClusterizationDimensionTypesService(IRepository<DimensionType> repository,
                                                   IRepository<EmbeddingModel> embeddingModelsRepository,
                                                   IMapper mapper)
        {
            _repository = repository;
            _embeddingModelsRepository = embeddingModelsRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ClusterizationDimensionTypeDTO>> GetAll()
        {
            var types = await _repository.GetAsync();

            return _mapper.Map<ICollection<ClusterizationDimensionTypeDTO>>(types);
        }

        public async Task<ICollection<ClusterizationDimensionTypeDTO>> GetAllInEmbeddingModel(string embeddingModelId)
        {
            var embeddingModel = await _embeddingModelsRepository.FindAsync(embeddingModelId);

            var types = await _repository.GetAsync(e => e.DimensionCount <= embeddingModel.DimensionTypeId);

            return _mapper.Map<ICollection<ClusterizationDimensionTypeDTO>>(types);
        }
    }
}
