using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Embeddings
{
    [DataContract(IsReference = true)]
    public class EmbeddingDimensionValue
    {
        public int Id { get; set; }

        public ClusterizationDimensionType DimensionType { get; set; }
        public int DimensionTypeId { get; set; }

        public EmbeddingData? EmbeddingData { get; set; }
        public int? EmbeddingDataId { get; set; }

        public DimensionalityReductionValue? DimensionalityReductionValue { get; set; }
        public int? DimensionalityReductionValueId { get; set; }
        public string ValuesString { get; set; }
    }
}
