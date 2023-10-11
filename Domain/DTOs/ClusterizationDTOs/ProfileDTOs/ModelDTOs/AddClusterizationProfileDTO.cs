using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs
{
    public class AddClusterizationProfileDTO
    {
        public int AlgorithmId { get; set; }
        public int DimensionCount { get; set; }
        public int WorkspaceId { get; set; }
    }
}
