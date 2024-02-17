using Domain.Entities.Customers;
using Domain.Entities.DimensionalityReduction;
using Domain.Entities.ExternalData;
using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationWorkspace
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public ClusterizationType Type { get; set; }
        public string TypeId { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<ExternalObject> ExternalObjects { get; set; } = new HashSet<ExternalObject>();

        public bool IsAllDataEmbedded { get; set; }

        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();
        public ICollection<ClusterizationEntity> Entities { get; set; } = new HashSet<ClusterizationEntity>();

        public int EntitiesCount { get; set; }

        public ICollection<ClusterizationWorkspaceDRTechnique> ClusterizationWorkspaceDRTechniques { get; set; } = new HashSet<ClusterizationWorkspaceDRTechnique>();
    
        public string VisibleType { get; set; }
        public string ChangingType { get; set; }

        public Customer Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
