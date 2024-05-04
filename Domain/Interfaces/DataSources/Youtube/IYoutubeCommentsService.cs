using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeCommentsService
    {
        public Task LoadFromVideo(YoutubeLoadOptions options);
        public Task LoadFromChannel(LoadYoutubeCommentsByChannelOptions options);

        public Task<YoutubeCommentDTO> GetLoadedById(string commentId);
        public Task<ICollection<YoutubeCommentDTO>> GetLoadedCollection(GetYoutubeCommentsRequest request);
    }
}
