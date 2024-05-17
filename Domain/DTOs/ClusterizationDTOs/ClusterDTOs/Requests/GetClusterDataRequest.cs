using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests
{
    public class GetClusterDataRequest
    {
        public int ClusterId { get; set; }
        public PageParameters PageParameters { get; set; }
    }
}
