using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.ChannelDTOs
{
    public class SimpleYoutubeChannelDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }

        public long SubscriberCount { get; set; }
        public int VideoCount { get; set; }
        public long ViewCount { get; set; }

        public int LoadedVideoCount { get; set; }
        public int LoadedCommentCount { get; set; }

        public bool IsLoaded { get; set; }

        public DateTime? LoadedDate { get; set; }

        public string ChannelImageUrl { get; set; }
    }
}
