using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Responses;
using Domain.Entities.Clusterization.Displaying;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClustersService
    {
        public Task<ICollection<ClusterDTO>> GetClusters(GetClustersRequest request);
        public Task<ICollection<ClusterDataDTO>> GetClusterEntities(GetClusterDataRequest request);
        public Task<ClusterListFileDTO> GetClustersFileModel(GetClustersFileRequest request);
    }
}
