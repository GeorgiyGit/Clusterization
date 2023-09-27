using Domain.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class GetVideosRequest
    {
        public string FilterStr { get; set; } = "";
        public string? ChannelId { get; set; }
        public string FilterType { get; set; } = ChannelFilterTypes.BySubscribersDesc;

        public PageParametersDTO PageParameters { get; set; }
    }
}
