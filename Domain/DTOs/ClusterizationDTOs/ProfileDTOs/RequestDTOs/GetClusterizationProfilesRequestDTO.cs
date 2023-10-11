using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs
{
    public class GetClusterizationProfilesRequestDTO
    {
        public int WorkspaceId { get; set; }
        public PageParametersDTO PageParameters { get; set; }

        public string? AlgorithmTypeId { get; set; }
        public int? DimensionCount { get; set; }
    }
}
