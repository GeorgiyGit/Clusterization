using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ModelDTOs
{
    public class SimpleClusterizationWorkspaceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
