using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.ModelDTOs;
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
    public class ClusterizationDimensionTypeService : IClusterizationDimensionTypeService
    {
        private readonly IRepository<ClusterizationDimensionType> repository;
        private readonly IMapper mapper;
        public ClusterizationDimensionTypeService(IRepository<ClusterizationDimensionType> repository,
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
