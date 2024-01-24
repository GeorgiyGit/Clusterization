using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Youtube
{
    [DataContract(IsReference = true)]
    public class Channel: YoutubePublishingDate
    {
        public string ETag { get; set; }
        public string Id { get; set; }

        public string Title { get; set; }
        public string? Country { get; set; }
        public string CustomUrl { get; set; }
        public string Description { get; set; }
        
        public DateTime PublishedAt { get; set; }
        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }

        public long SubscriberCount { get; set; }
        public int VideoCount { get; set; }
        public int LoadedVideoCount { get; set; }
        public int LoadedCommentCount { get; set; }
        public long ViewCount { get; set; }

        public DateTime LoadedDate { get; set; }

        public string ChannelImageUrl { get; set; }

        public ICollection<Video> Videos { get; set; } = new HashSet<Video>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
