using Domain.Entities.Clusterization;
using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Embeddings
{
    public class EmbeddingData
    {
        public int Id { get; set; }

        public ICollection<EmbeddingDimensionValue> Embeddings { get; set; } = new HashSet<EmbeddingDimensionValue>();

        public Comment Comment { get; set; }
        public string CommentId { get; set; }

        public ICollection<ClusterizationEntity> Entities { get; set; } = new HashSet<ClusterizationEntity>();
    }
}
