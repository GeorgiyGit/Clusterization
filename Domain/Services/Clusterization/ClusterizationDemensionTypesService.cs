using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DemensionTypeDTO;
using Domain.Entities.Clusterization;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization
{
    public class ClusterizationDimensionTypesService : IClusterizationDimensionTypesService
    {
        private readonly IRepository<ClusterizationDimensionType> repository;
        private readonly IMapper mapper;
        public ClusterizationDimensionTypesService(IRepository<ClusterizationDimensionType> repository,
                                                   IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ICollection<ClusterizationDimensionTypeDTO>> GetAll()
        {
            var types = await repository.GetAsync();

            return mapper.Map<ICollection<ClusterizationDimensionTypeDTO>>(types);
        }
    }
}
