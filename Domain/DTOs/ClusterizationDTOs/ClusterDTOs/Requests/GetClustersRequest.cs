using Domain.Entities.Clusterization.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests
{
    public class GetClustersRequest
    {
        public int ProfileId { get; set; }
        public PageParameters PageParameters { get; set; }
    }
}
