using Domain.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs
{
    public class ClusterizationWorkspaceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public int EntitiesCount { get; set; }
        public int ProfilesCount { get; set; }
        public bool IsAllDataEmbedded { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
        public string OwnerId { get; set; }
    }
}
