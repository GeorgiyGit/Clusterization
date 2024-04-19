using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.DTOs.ExternalData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Workspaces
{
    public interface IClusterizationWorkspacesService
    {
        public Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCollection(GetWorkspacesRequest request);
        public Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCustomerCollection(GetWorkspacesRequest request);
        public Task<ClusterizationWorkspaceDTO> GetFullById(int id);
        public Task<SimpleClusterizationWorkspaceDTO> GetSimpleById(int id);

        public Task Add(AddClusterizationWorkspaceRequest model);
        public Task Update(UpdateClusterizationWorkspaceRequest model);

        public Task<ICollection<string>> GetAllDataObjectsInList(int id);
    }
}
