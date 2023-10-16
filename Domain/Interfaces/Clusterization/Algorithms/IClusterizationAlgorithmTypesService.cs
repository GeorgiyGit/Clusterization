using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Algorithms
{
    public interface IClusterizationAlgorithmTypesService
    {
        public Task<ICollection<SimpleAlgorithmTypeDTO>> GetAll();
    }
}
