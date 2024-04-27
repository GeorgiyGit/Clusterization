using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeDataObjectsService
    {
        public Task LoadCommentsByChannel(AddYoutubeCommentsToWorkspaceByChannelRequest request);
        public Task LoadCommentsByVideos(AddYoutubeCommentsToWorkspaceByVideosRequest request);
    }
}
