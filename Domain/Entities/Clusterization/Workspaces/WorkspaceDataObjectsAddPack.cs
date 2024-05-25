using Domain.Entities.Customers;
using Domain.Entities.DataObjects;
using Domain.Entities.Embeddings;
using Domain.Entities.Monitorings;
using Domain.Entities.Tasks;

namespace Domain.Entities.Clusterization.Workspaces
{
    public class WorkspaceDataObjectsAddPack : Monitoring
    {
        public int Id { get; set; }

        public ICollection<MyDataObject> DataObjects { get; set; } = new HashSet<MyDataObject>();
        public int DataObjectsCount { get; set; }

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }

        public Customer Owner { get; set; }
        public string OwnerId { get; set; }

        public ICollection<EmbeddingLoadingState> EmbeddingLoadingStates { get; set; } = new HashSet<EmbeddingLoadingState>();

        public ICollection<MyBaseTask> Tasks { get; set; } = new HashSet<MyBaseTask>();
    }
}
