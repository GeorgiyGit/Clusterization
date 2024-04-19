using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Youtube;
using Domain.Entities.Embeddings;

namespace Domain.Entities.DataObjects
{
    public class MyDataObject
    {
        public long Id { get; set; }
        public string Text { get; set; }

        public MyDataObjectType Type { get; set; }
        public string TypeId { get; set; }

        public Comment? Comment { get; set; }
        public string? CommentId { get; set; }

        public ExternalObject? ExternalObject { get; set; }
        public string? ExternalObjectId { get; set; }

        public ICollection<EmbeddingObjectsGroup> EmbeddingObjectsGroups { get; set; } = new HashSet<EmbeddingObjectsGroup>();
        
        public ICollection<WorkspaceDataObjectsAddPack> WorkspaceDataObjectsAddPacks { get; set; } = new HashSet<WorkspaceDataObjectsAddPack>();

        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();

        public ICollection<Cluster> Clusters { get; set; } = new HashSet<Cluster>();

        public ICollection<DisplayedPoint> DisplayedPoints { get; set; } = new HashSet<DisplayedPoint>();
    }
}
