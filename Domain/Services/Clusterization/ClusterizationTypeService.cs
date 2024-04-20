using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Other;

namespace Domain.Services.Clusterization
{
    public class ClusterizationTypeService : IClusterizationTypesService
    {
        private readonly IRepository<ClusterizationType> repository;
        private readonly IMapper mapper;
        public ClusterizationTypeService(IRepository<ClusterizationType> repository,
                                         IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ICollection<ClusterizationTypeDTO>> GetAll()
        {
            var types = await repository.GetAsync();

            return mapper.Map<ICollection<ClusterizationTypeDTO>>(types);
        }
    }
}
