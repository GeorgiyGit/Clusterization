using Domain.DTOs.ClusterizationDTOs.FastClusteringDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.ExternalData;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Entities.Clusterization.FastClustering;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types.Clusterization;
using Domain.Resources.Types.Tasks;
using Domain.Services.TaskServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IFastClusteringService
    {
        public Task<int> CreateWorkflow();
        public Task<int> InitializeWorkspace(FastClusteringInitialRequest request);
        public Task<int> InitializeProfile(FastClusteringProcessRequest request);
    }
}
