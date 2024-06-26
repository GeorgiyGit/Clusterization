﻿
namespace Domain.DTOs.YoutubeDTOs.CommentDTOs
{
    public class YoutubeCommentDTO
    {
        public string Id { get; set; }

        public short TotalReplyCount { get; set; }

        public string AuthorDisplayName { get; set; }
        public string AuthorProfileImageUrl { get; set; }

        public int LikeCount { get; set; }

        public DateTimeOffset PublishedAtDateTimeOffset { get; set; }
        public string PublishedAtRaw { get; set; }

        public string TextDisplay { get; set; }
        public string TextOriginal { get; set; }

        public string ChannelId { get; set; }
        public string VideoId { get; set; }
    }
}
