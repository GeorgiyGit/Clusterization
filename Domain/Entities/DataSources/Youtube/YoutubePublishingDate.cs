
namespace Domain.Entities.DataSources.Youtube
{
    public interface YoutubePublishingDate
    {
        public DateTime PublishedAt { get; set; }
        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }
    }
}
