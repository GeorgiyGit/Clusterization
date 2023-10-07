using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.Entities.Clusterization.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class AlgorithmTypeProfile : AutoMapper.Profile
    {
        public AlgorithmTypeProfile()
        {
            CreateMap<ClusterizationAlgorithmType, SimpleAlgoritmTypeDTO>();
        }
    }
}
