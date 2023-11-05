using AutoMapper.Configuration.Conventions;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.DTOs.DimensionalityReductionDTOs.TechniqueDTOs;
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

        public DimensionalityReductionTechniqueDTO DimensionalityReductionTechnique { get; set; }
        public SimpleAlgorithmTypeDTO AlgorithmType { get; set; }
    }
}
