using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeCommentsService
    {
        public Task LoadFromVideo(LoadOptions options);
        public Task LoadFromChannel(LoadCommentsByChannelOptions options);

        public Task<CommentDTO> GetLoadedById(string commentId);
        public Task<ICollection<CommentDTO>> GetLoadedCollection(GetCommentsRequest request);
    }
}
