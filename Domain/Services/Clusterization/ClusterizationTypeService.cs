using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Other;

namespace Domain.Services.Clusterization
{
    public class ClusterizationTypeService : IClusterizationTypesService
    {
        private readonly IRepository<ClusterizationType> _repository;
        private readonly IMapper _mapper;
        public ClusterizationTypeService(IRepository<ClusterizationType> repository,
                                         IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<ClusterizationTypeDTO>> GetAll()
        {
            var types = await _repository.GetAsync();

            return _mapper.Map<ICollection<ClusterizationTypeDTO>>(types);
        }
    }
}
