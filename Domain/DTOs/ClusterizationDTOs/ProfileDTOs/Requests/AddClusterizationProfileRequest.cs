using Domain.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs
{
    public class AddClusterizationProfileRequest
    {
        public int AlgorithmId { get; set; }
        public string DRTechniqueId { get; set; }
        public string EmbeddingModelId { get; set; } 
        public int DimensionCount { get; set; }
        public int WorkspaceId { get; set; }

        public string VisibleType { get; set; } = VisibleTypes.AllCustomers;
        public string ChangingType { get; set; } = ChangingTypes.AllCustomers;
    }
}
