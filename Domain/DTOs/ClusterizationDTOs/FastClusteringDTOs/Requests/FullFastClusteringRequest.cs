using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.FastClusteringDTOs.Requests
{
    public class FullFastClusteringRequest
    {
        public int AlgorithmId { get; set; }
        public string DRTechniqueId { get; set; }
        public string EmbeddingModelId { get; set; }
        public int DimensionCount { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public List<string> Texts { get; set; } = new List<string>();
    }
}
