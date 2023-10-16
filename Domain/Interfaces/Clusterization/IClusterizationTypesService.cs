using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationTypesService
    {
        public Task<ICollection<ClusterizationTypeDTO>> GetAll();
    }
}
