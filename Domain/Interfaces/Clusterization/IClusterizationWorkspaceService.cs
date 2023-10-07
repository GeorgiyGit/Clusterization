using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationWorkspaceService
    {
        public Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetWorkspaces(GetWorkspacesRequest request);
        public Task<ClusterizationWorkspaceDTO> GetById(int id);

        public Task Add(AddClusterizationWorkspaceDTO model);
        public Task Update(UpdateClusterizationWorkspaceDTO model);
        
        public Task LoadCommentsByVideos(AddCommentsToWorkspaceByVideosRequest request);
        public Task LoadCommentsByChannel(AddCommentsToWorkspaceByChannelRequest request);
    }
}
