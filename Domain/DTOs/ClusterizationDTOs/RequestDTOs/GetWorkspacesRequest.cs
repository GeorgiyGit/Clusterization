using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.RequestDTOs
{
    public class GetWorkspacesRequest
    {
        public string? TypeId { get; set; }
        public PageParametersDTO PageParameters { get; set; }
    }
}
