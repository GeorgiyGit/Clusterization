using AutoMapper.Configuration.Conventions;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.DimensionalityReduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationProfile
    {
        public int Id { get; set; }
        
        public ClusterizationAbstactAlgorithm Algorithm { get; set; }
        public int AlgorithmId { get; set; }

        public ClusterizationDimensionType DimensionType { get; set; }
        public int DimensionCount { get; set; }

        public DimensionalityReductionTechnique DimensionalityReductionTechnique { get; set; }
        public string DimensionalityReductionTechniqueId { get; set; }

        public ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
        public ICollection<ClusterizationTile> Tiles { get; set; } = new HashSet<ClusterizationTile>();

        public ICollection<ClusterizationTilesLevel> TilesLevels { get; set; } = new HashSet<ClusterizationTilesLevel>();

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }

        public bool IsCalculated { get; set; }

        public int MinTileLevel { get; set; }
        public int MaxTileLevel { get; set; }
    }
}
