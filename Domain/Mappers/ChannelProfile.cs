using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    internal class ChannelProfile : AutoMapper.Profile
    {
        public ChannelProfile()
        {
            CreateMap<Channel, SimpleChannelDTO>()
                    .ForMember(dest => dest.LoadedVideoCount,
                               ost => ost.MapFrom(e => e.Videos.Count()))
                    .ForMember(dest => dest.LoadedCommentCount,
                               ost => ost.MapFrom(e => e.Comments.Count()))
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());
        }
    }
}
