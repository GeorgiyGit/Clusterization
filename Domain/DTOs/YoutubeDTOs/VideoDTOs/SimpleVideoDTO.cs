using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.VideoDTOs
{
    public class SimpleVideoDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }

        public string VideoImageUrl { get; set; }

        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public long ViewCount { get; set; }

        public bool IsLoaded { get; set; }

        public DateTime? LoadedDate { get; set; }

        public int LoadedCommentCount { get; set; }
    }
}
