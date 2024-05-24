using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.FastClusteringDTOs.Requests
{
    public class FastClusteringInitialRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<string> Texts { get; set; } = new List<string>();
    }
}
