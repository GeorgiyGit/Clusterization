using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Mappers
{
    internal class YoutubeCommentProfile : AutoMapper.Profile
    {
        public YoutubeCommentProfile()
        {
            CreateMap<YoutubeComment, YoutubeCommentDTO>();
        }
    }
}
