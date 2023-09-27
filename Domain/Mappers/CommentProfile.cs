using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
