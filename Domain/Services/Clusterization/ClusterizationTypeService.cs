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
    public class ClusterizationTypeService : IClusterizationTypeService
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
