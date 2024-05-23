using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Requests
{
    public class GetAlgorithmsRequest
    {
        public string TypeId { get; set; }
        public PageParameters PageParameters { get; set; }
    }
}
