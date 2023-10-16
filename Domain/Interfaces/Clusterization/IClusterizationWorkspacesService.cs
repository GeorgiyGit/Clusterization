using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationWorkspacesService
    {
        public Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCollection(GetWorkspacesRequest request);
        public Task<ClusterizationWorkspaceDTO> GetFullById(int id);

        public Task Add(AddClusterizationWorkspaceDTO model);
        public Task Update(UpdateClusterizationWorkspaceDTO model);
        
        public Task LoadCommentsByVideos(AddCommentsToWorkspaceByVideosRequest request);
        public Task LoadCommentsByChannel(AddCommentsToWorkspaceByChannelRequest request);
    }
}
