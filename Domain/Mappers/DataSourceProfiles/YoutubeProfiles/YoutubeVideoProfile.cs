using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Mappers
{
    public class YoutubeVideoProfile : AutoMapper.Profile
    {
        public YoutubeVideoProfile()
        {
            CreateMap<YoutubeVideo, SimpleYoutubeVideoDTO>()
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());
        }
    }
}
