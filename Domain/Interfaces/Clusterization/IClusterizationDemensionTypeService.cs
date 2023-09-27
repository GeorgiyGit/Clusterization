using Domain.DTOs.ClusterizationDTOs.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationDimensionTypeService
    {
        public Task<ICollection<ClusterizationDimensionTypeDTO>> GetAll();
    }
}
