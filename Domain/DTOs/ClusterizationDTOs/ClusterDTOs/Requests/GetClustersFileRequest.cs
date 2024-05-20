using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;

namespace Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests
{
    public class GetClustersFileRequest
    {
        public int ProfileId { get; set; }
        public ICollection<int> ClusterIds { get; set; } = new List<int>();
    }
}
