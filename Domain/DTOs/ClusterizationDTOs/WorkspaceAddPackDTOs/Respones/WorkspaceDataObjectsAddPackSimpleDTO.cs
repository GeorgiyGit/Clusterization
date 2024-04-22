using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Respones
{
    public class WorkspaceDataObjectsAddPackSimpleDTO
    {
        public int Id { get; set; }

        public int DataObjectsCount { get; set; }

        public SimpleCustomerDTO Owner { get; set; }

        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public string WorkspaceChangingType { get; set; }
    }
}
