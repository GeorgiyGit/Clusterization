using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Customers;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Entities.DimensionalityReductionEntities;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationProfile
    {
        public int Id { get; set; }

        public ClusterizationAbstactAlgorithm Algorithm { get; set; }
        public int AlgorithmId { get; set; }

        public DimensionType DimensionType { get; set; }
        public int DimensionCount { get; set; }

        public DimensionalityReductionTechnique DRTechnique { get; set; }
        public string DRTechniqueId { get; set; }

        public ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
        public ICollection<ClusterizationTile> Tiles { get; set; } = new HashSet<ClusterizationTile>();

        public ICollection<ClusterizationTilesLevel> TilesLevels { get; set; } = new HashSet<ClusterizationTilesLevel>();

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }

        public bool IsCalculated { get; set; }

        public int MinTileLevel { get; set; }
        public int MaxTileLevel { get; set; }

        public bool IsElected { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }

        public Customer Owner { get; set; }
        public string OwnerId { get; set; }

        public EmbeddingModel EmbeddingModel { get; set; }
        public string EmbeddingModelId { get; set; }

        public EmbeddingLoadingState EmbeddingLoadingState { get; set; }
        public int EmbeddingLoadingStateId { get; set; }
    }
}
