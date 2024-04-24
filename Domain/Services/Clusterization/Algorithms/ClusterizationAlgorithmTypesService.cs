using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Other;

namespace Domain.Services.Clusterization.Algorithms
{
    public class ClusterizationAlgorithmTypesService : IClusterizationAlgorithmTypesService
    {
        private readonly IRepository<ClusterizationAlgorithmType> _repository;
        private readonly IMapper _mapper;
        public ClusterizationAlgorithmTypesService(IRepository<ClusterizationAlgorithmType> repository,
                                                   IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<SimpleAlgorithmTypeDTO>> GetAll()
        {
            var types = await _repository.GetAsync();

            return _mapper.Map<ICollection<SimpleAlgorithmTypeDTO>>(types);
        }
    }
}
