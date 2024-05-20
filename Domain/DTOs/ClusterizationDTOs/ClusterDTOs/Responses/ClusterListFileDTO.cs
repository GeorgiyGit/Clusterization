using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Responses
{
    public class ClusterListFileDTO
    {
        public ICollection<ClusterFileDTO> Clusters { get; set; } = new List<ClusterFileDTO>();
    }
}
