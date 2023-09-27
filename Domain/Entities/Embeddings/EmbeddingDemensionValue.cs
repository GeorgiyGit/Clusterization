using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Embeddings
{
    public class EmbeddingDimensionValue
    {
        public int Id { get; set; }

        public ClusterizationDimensionType DimensionType { get; set; }
        public int DimensionTypeId { get; set; }

        public EmbeddingData EmbeddingData { get; set; }
        public int EmbeddingDataId { get; set; }

        public ICollection<EmbeddingValue> Values { get; set; } = new HashSet<EmbeddingValue>();
    }
}
