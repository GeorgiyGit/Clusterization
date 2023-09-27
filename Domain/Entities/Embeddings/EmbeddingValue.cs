using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Embeddings
{
    public class EmbeddingValue
    {
        public int Id { get; set; }
        public double Value { get; set; }

        public EmbeddingDimensionValue EmbeddingDimensionValue { get; set; }
        public int EmbeddingDimensionValueId { get; set; }
    }
}
