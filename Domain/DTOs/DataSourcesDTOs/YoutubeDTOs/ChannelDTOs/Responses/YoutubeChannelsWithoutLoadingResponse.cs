using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Responses
{
    public class YoutubeChannelsWithoutLoadingResponse
    {
        public ICollection<SimpleYoutubeChannelDTO> Channels { get; set; } = new List<SimpleYoutubeChannelDTO>();
        public string? NextPageToken { get; set; }
    }
}
