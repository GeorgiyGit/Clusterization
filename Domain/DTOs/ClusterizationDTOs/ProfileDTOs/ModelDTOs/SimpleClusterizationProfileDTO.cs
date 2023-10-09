using AutoMapper.Configuration.Conventions;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
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
        public string AlgorithmName { get; set; }
        public int DimensionId { get; set; }

        public SimpleAlgorithmTypeDTO AlgorithmTypeDTO;
    }
}
