using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Requests
{
    public class GetWorkspaceDataObjectsAddPacksRequest
    {
        public int WorkspaceId { get; set; }
        public PageParameters PageParameters { get; set; }
    }
}
