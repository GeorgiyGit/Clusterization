using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Other;

namespace Domain.Services.Clusterization.Algorithms
{
    public class ClusterizationAlgorithmTypesService : IClusterizationAlgorithmTypesService
    {
        private readonly IRepository<ClusterizationAlgorithmType> repository;
        private readonly IMapper mapper;
        public ClusterizationAlgorithmTypesService(IRepository<ClusterizationAlgorithmType> repository,
                                                   IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ICollection<SimpleAlgorithmTypeDTO>> GetAll()
        {
            var types = await repository.GetAsync();

            return mapper.Map<ICollection<SimpleAlgorithmTypeDTO>>(types);
        }
    }
}
