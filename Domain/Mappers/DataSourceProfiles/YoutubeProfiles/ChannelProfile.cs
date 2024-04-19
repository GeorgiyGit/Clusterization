using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.Entities.DataSources.Youtube;

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
