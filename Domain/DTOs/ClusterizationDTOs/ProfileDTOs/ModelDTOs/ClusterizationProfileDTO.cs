using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs
{
    public class ClusterizationProfileDTO
    {
        public int Id { get; set; }

        public int AlgorithmId { get; set; }

        public SimpleAlgorithmTypeDTO AlgorithmType { get; set; }

        public int DimensionTypeId { get; set; }

        public int ClustersCount { get; set; }

        public int WorkspaceId { get; set; }

        public bool IsCalculated { get; set; }
        public bool IsFullyCalculated { get; set; }

        public int MinTileLevel { get; set; }
        public int MaxTileLevel { get; set; }
    }
}
