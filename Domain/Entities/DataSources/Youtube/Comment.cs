using Domain.Entities.Customers;
using Domain.Entities.DataObjects;

namespace Domain.Entities.DataSources.Youtube
{
    public class Comment: YoutubePublishingDate
    {
        public string Id { get; set; }
        public string ETag { get; set; }
        
        public bool CanReply { get; set; }
        public short TotalReplyCount { get; set; }
        
        public string AuthorChannelUrl { get; set; }
        public string AuthorDisplayName { get; set; }
        public string AuthorProfileImageUrl { get; set; }
        public string AuthorChannelId { get; set; }

        public int LikeCount { get; set; }

        public string TextDisplay { get; set; }
        public string TextOriginal { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTimeOffset UpdatedAtDateTimeOffset { get; set; }
        public string UpdatedAtRaw { get; set; }

        public Channel Channel { get; set; }
        public string ChannelId { get; set; }   

        public Video Video { get; set; }
        public string VideoId { get; set; }

        public DateTime PublishedAt { get; set; }
        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }

        public DateTime LoadedDate { get; set; }

        public MyDataObject? DataObject { get; set; }
        public long? DataObjectId { get; set; }

        public Customer Loader { get; set; }
        public string LoaderId { get; set; }
    }
}
