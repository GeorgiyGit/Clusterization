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
    public class ChannelProfile : AutoMapper.Profile
    {
        public ChannelProfile()
        {
            CreateMap<Channel, SimpleChannelDTO>()
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());
        }
    }
}
