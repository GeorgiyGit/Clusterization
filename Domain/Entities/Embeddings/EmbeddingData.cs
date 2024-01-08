using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using Domain.Entities.ExternalData;
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

        public EmbeddingDimensionValue OriginalEmbedding { get; set; }
        public int OriginalEmbeddingId { get; set; }

        public DimensionalityReductionValue DimensionalityReductionValue { get; set; }
        public int DimensionalityReductionValueId { get; set; }

        public Comment? Comment { get; set; }
        public string? CommentId { get; set; }

        public ExternalObject? ExternalObject { get; set; }
        public string? ExternalObjectId { get; set; }

        public ICollection<ClusterizationEntity> Entities { get; set; } = new HashSet<ClusterizationEntity>();
    }
}
