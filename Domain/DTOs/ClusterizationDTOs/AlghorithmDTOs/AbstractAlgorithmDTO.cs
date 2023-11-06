using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs
{
    public class AbstractAlgorithmDTO
    {
        public int Id { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string FullTitle { get; set; }
    }
}
