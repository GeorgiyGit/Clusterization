using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings.DimensionEntities;
using System.Security.Cryptography.Xml;
using Domain.Entities.DimensionalityReductionEntities;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;

namespace Domain.Entities.Embeddings
{
    public class EmbeddingObjectsGroup
    {
        public long Id { get; set; }
        
        public MyDataObject DataObject { get; set; }
        public long DataObjectId { get; set; }

        public DimensionalityReductionTechnique DRTechnique { get; set; }
        public string DRTechniqueId { get; set; }

        public EmbeddingModel EmbeddingModel { get; set; }
        public string EmbeddingModelId { get; set; }

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }

        public ICollection<DimensionEmbeddingObject> DimensionEmbeddingObjects { get; set; } = new HashSet<DimensionEmbeddingObject>();
    }
}
