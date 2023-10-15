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
    public class Video: YoutubePublishingDate
    {
        public string ETag { get; set; }
        public string Id { get; set; }
        
        public string ChannelTitle { get; set; }

        public string CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string LiveBroadcaseContent { get; set; }

        public DateTime PublishedAt { get; set; }
        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }

        public string? DefaultAudioLanguage { get; set; }
        public string? DefaultLanguage { get; set; }

        public string VideoImageUrl { get; set; }

        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public long ViewCount { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public Channel Channel { get; set; }
        public string ChannelId { get; set; }

        public DateTime LoadedDate { get; set; }
    }
}
