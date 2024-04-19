using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs
{
    public class GetWorkspacesRequest
    {
        public string? TypeId { get; set; }
        public PageParameters PageParameters { get; set; }
        public string FilterStr { get; set; } = "";
    }
}
