using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Algorithms
{
    public interface IGeneralClusterizationAlgorithmService
    {
        public Task<AbstractAlgorithmDTO> GetAllAlgorithms(string typeId);
    }
}
