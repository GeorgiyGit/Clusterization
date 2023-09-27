using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationDimensionType
    {
        public int DimensionCount { get; set; }

        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();
        public ICollection<EmbeddingDimensionValue> DimensionValues { get; set; }= new HashSet<EmbeddingDimensionValue>();
    }
}
