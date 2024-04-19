using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Mappers
{
    internal class CommentProfile : AutoMapper.Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>();
        }
    }
}
