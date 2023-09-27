using AutoMapper.Configuration.Conventions;
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
        
        public ClusterizationAlgorithm Algorithm { get; set; }
        public string AlgorithmId { get; set; }

        public ClusterizationDimensionType DimensionType { get; set; }
        public int DimensionTypeId { get; set; }


        public ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();

        public ICollection<ClusterizationPointColors> PointColorsCollection { get; set; } = new HashSet<ClusterizationPointColors>();

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }

        public ICollection<ClusterizationTile> Tiles { get; set; } = new HashSet<ClusterizationTile>();
    }
}
