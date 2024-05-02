using AutoMapper.Configuration.Conventions;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
using Domain.DTOs.EmbeddingModelDTOs.Responses;
using Domain.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs
{
    public class SimpleClusterizationProfileDTO
    {
        public int Id { get; set; }
        public int DimensionCount { get; set; }
        public string FullTitle { get; set; }

        public DimensionalityReductionTechniqueDTO DRTechnique { get; set; }
        public SimpleAlgorithmTypeDTO AlgorithmType { get; set; }
        public EmbeddingModelDTO EmbeddingModel { get; set; }

        public bool IsElected { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
        public string OwnerId { get; set; }

        public bool IsInCalculation { get; set; }
    }
}
