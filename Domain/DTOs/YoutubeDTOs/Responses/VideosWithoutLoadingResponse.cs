using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Responses
{
    public class VideosWithoutLoadingResponse
    {
        public ICollection<SimpleVideoDTO> Videos { get; set; } = new List<SimpleVideoDTO>();
        public string? NextPageToken { get; set; }
    }
}
