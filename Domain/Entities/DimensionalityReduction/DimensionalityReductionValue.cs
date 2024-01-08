using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DimensionalityReduction
{
    public class DimensionalityReductionValue
    {
        public int Id { get; set; }

        public ICollection<EmbeddingDimensionValue> Embeddings { get; set; } = new HashSet<EmbeddingDimensionValue>();

        public EmbeddingData? EmbeddingData { get; set; }
        public int? EmbeddingDataId { get; set; }

        public ClusterizationEntity? ClusterizationEntity { get; set; }
        public int? ClusterizationEntityId { get; set; }

        public ClusterizationWorkspaceDRTechnique? ClusterizationWorkspaceDRTechnique { get; set; }
        public int? ClusterizationWorkspaceDRTechniqueId { get; set; }

        public DimensionalityReductionTechnique Technique { get; set; }
        public string TechniqueId { get; set; }
    }
}
