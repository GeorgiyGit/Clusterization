using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class LoadYoutubeCommentsByChannelOptions : YoutubeLoadOptions
    {
        public bool IsVideoDateCount { get; set; } //load by video date, or by comments date
        public int MaxLoadForOneVideo { get; set; }
    }
}
