using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Youtube
{
    public interface YoutubePublishingDate
    {
        public DateTime PublishedAt { get; set; }
        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }
    }
}
