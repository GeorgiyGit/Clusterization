using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeDataObjectsService
    {
        public Task LoadCommentsByChannel(AddCommentsToWorkspaceByChannelRequest request);
        public Task LoadCommentsByVideos(AddCommentsToWorkspaceByVideosRequest request);
    }
}
