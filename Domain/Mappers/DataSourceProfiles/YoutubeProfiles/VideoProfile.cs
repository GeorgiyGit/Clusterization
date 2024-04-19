using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Mappers
{
    public class VideoProfile : AutoMapper.Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, SimpleVideoDTO>()
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());
        }
    }
}
