using Domain.Resources.Types.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.FastClusteringDTOs.Requests
{
    public class FastClusteringProcessRequest
    {
        public int AlgorithmId { get; set; }
        public string DRTechniqueId { get; set; }
        public string EmbeddingModelId { get; set; }
        public int DimensionCount { get; set; }
        public int WorkspaceId { get; set; }
    }
}
