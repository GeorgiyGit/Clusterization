using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Algorithms
{
    public interface IGeneralClusterizationAlgorithmService
    {
        public Task<ICollection<AbstractAlgorithmDTO>> GetAllAlgorithms(string typeId);
        public Task<SimpleAlgorithmTypeDTO?> GetAlgorithmTypeByAlgorithmId(int algorithmId);

        public Task<int> CalculateQuotasCount(string algorithmTypeId,int dataObjectsCount, int dimensionCount);
        public Task<int> CalculateQuotasCountByWorkspace(string algorithmTypeId, int workspaceId, int dimensionCount);
    }
}
