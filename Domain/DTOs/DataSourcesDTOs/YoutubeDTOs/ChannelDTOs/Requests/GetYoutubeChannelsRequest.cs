﻿using Domain.Resources.Types;
using Domain.Resources.Types.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class GetYoutubeChannelsRequest
    {
        public string FilterStr { get; set; } = "";
        public string FilterType { get; set; } = ChannelFilterTypes.BySubscribersDesc;
    
        public PageParameters PageParameters { get; set; }
    }
}