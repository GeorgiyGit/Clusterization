using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
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

        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
        public ICollection<ClusterizationEntity> ClusterizationEntities { get; set; } = new HashSet<ClusterizationEntity>();
        
        public EmbeddingData? EmbeddingData { get; set; }
        public int? EmbeddingDataId { get; set; }
    }
}
