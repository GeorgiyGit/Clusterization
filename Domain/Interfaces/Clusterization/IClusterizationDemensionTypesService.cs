using Domain.DTOs.ClusterizationDTOs.DemensionTypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationDimensionTypesService
    {
        public Task<ICollection<ClusterizationDimensionTypeDTO>> GetAll();
    }
}
