﻿using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.Entities.Clusterization.Algorithms;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class OneClusterProfile : AutoMapper.Profile
    {
        public OneClusterProfile()
        {
            CreateMap<OneClusterAlgorithm, OneClusterAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>()
                .ForMember(dest => dest.FullTitle,
                           ost => ost.MapFrom(e => e.Type.Name + " " + e.ClusterColor));
        }
    }
}
