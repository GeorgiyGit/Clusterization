using Domain.Resources.Types;
using Domain.Resources.Types.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class GetYoutubeCommentsRequest
    {
        public PageParameters PageParameters { get; set; }

        public string FilterStr { get; set; } = "";
        public string FilterType { get; set; } = CommentFilterTypes.OrderUnspecified;

        public string? AuthorId { get; set; }
        public string? ChannelId { get; set; }
        public string? ParentId { get; set; }
        public string? VideoId { get; set; }
    }
}
