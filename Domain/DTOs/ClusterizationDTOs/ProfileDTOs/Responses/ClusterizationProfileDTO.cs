﻿using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.Resources.Types;
using Domain.DTOs.EmbeddingModelDTOs.Responses;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs
{
    public class ClusterizationProfileDTO
    {
        public int Id { get; set; }

        public int AlgorithmId { get; set; }
        public SimpleAlgorithmTypeDTO AlgorithmType { get; set; }

        public int DimensionCount { get; set; }

        public int ClustersCount { get; set; }

        public int WorkspaceId { get; set; }

        public bool IsCalculated { get; set; }
        public bool IsFullyCalculated { get; set; }

        public int MinTileLevel { get; set; }
        public int MaxTileLevel { get; set; }

        public DimensionalityReductionTechniqueDTO DRTechnique { get; set; }
        
        public bool IsElected { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
        public string OwnerId { get; set; }

        public EmbeddingModelDTO EmbeddingModel { get; set; }
        public bool IsAllEmbeddingsLoaded { get; set; }

        public bool IsInCalculation { get; set; }
    }
}
