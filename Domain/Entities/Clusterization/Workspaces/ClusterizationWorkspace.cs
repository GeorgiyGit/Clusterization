using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Customers;
using Domain.Entities.DataObjects;
using Domain.Entities.Embeddings;
using Domain.Entities.Monitorings;

namespace Domain.Entities.Clusterization.Workspaces
{
    public class ClusterizationWorkspace : Monitoring
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public ClusterizationType Type { get; set; }
        public string TypeId { get; set; }

        public bool IsAllDataEmbedded { get; set; }

        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();

        public int EntitiesCount { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }

        public Customer Owner { get; set; }
        public string OwnerId { get; set; }

        public ICollection<EmbeddingObjectsGroup> EmbeddingObjectsGroups { get; set; } = new HashSet<EmbeddingObjectsGroup>();

        public ICollection<MyDataObject> DataObjects { get; set; } = new HashSet<MyDataObject>();

        public ICollection<WorkspaceDataObjectsAddPack> WorkspaceDataObjectsAddPacks { get; set; } = new HashSet<WorkspaceDataObjectsAddPack>();
    }
}
