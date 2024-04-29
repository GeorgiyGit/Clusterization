using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs
{
    public class CalculateQuotasCountRequest
    {
        public string AlgorithmTypeId { get; set; }
        public int? EntitiesCount { get; set; }
        public int? WorkspaceId { get; set; }
        public int DimensionCount { get; set; }
    }
}
