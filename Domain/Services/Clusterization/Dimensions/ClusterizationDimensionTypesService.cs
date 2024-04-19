using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.Entities.Clusterization;
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
        private readonly IRepository<DimensionType> repository;
        private readonly IMapper mapper;
        public ClusterizationDimensionTypesService(IRepository<DimensionType> repository,
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
