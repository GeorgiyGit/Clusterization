using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    internal class VideoProfile : AutoMapper.Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, SimpleVideoDTO>()
                    .ForMember(dest => dest.LoadedCommentCount,
                               ost => ost.MapFrom(e => e.Comments.Count()))
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());
        }
    }
}
