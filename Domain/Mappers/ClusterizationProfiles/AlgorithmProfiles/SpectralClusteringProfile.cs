using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.Entities.Clusterization.Algorithms;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class SpectralClusteringProfile : AutoMapper.Profile
    {
        public SpectralClusteringProfile()
        {
            CreateMap<SpectralClusteringAlgorithm, SpectralClusteringAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>()
            .ForMember(dest => dest.FullTitle,
                          ost => ost.MapFrom(e => e.Type.Name + " " + e.NumClusters + " " + e.Gamma));
        }
    }
}
