using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Responses
{
    public class ClusterDataDTO
    {
        public ClusterDTO? Cluster { get; set; }
        public ClusterDataObjectDTO? DataObject { get; set; }
    }
}
