using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Responses
{
    public class ChannelsWithoutLoadingResponse
    {
        public ICollection<SimpleChannelDTO> Channels { get; set; } = new List<SimpleChannelDTO>();
        public string? NextPageToken { get; set; }
    }
}
