﻿using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.TaskDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class KMeansProfile : AutoMapper.Profile
    {
        public KMeansProfile()
        {
            CreateMap<KMeansAlgorithm,KMeansAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>();
        }
    }
}