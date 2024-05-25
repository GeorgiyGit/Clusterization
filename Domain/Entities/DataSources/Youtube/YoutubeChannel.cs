using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using System.Runtime.Serialization;

namespace Domain.Entities.DataSources.Youtube
{
    [DataContract(IsReference = true)]
    public class YoutubeChannel: YoutubePublishingDate
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

        public ICollection<YoutubeVideo> Videos { get; set; } = new HashSet<YoutubeVideo>();
        public ICollection<YoutubeComment> Comments { get; set; } = new HashSet<YoutubeComment>();

        public Customer Loader { get; set; }
        public string LoaderId { get; set; }

        public ICollection<MyBaseTask> Tasks { get; set; } = new HashSet<MyBaseTask>();
    }
}
