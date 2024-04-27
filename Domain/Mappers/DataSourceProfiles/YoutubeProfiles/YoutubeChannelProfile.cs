using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Mappers
{
    public class YoutubeChannelProfile : AutoMapper.Profile
    {
        public YoutubeChannelProfile()
        {
            CreateMap<YoutubeChannel, SimpleYoutubeChannelDTO>()
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());
        }
    }
}
