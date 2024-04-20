using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    internal class ClusterizationProfileMapperProfile : AutoMapper.Profile
    {
        public ClusterizationProfileMapperProfile()
        {
            CreateMap<ClusterizationProfile, ClusterizationProfileDTO>()
                .ForMember(dest => dest.AlgorithmType,
                           ost => ost.Ignore())
                .ForMember(dest => dest.ClustersCount,
                           ost => ost.MapFrom(e => e.Clusters.Count()))
                .ForMember(dest => dest.IsAllEmbeddingsLoaded,
                           ost => ost.MapFrom(e => e.EmbeddingLoadingState.IsAllEmbeddingsLoaded));


            CreateMap<ClusterizationProfile, SimpleClusterizationProfileDTO>()
                .ForMember(dest => dest.AlgorithmType,
                           ost => ost.Ignore())
                .ForMember(dest => dest.FullTitle,
                           ost => ost.Ignore());
        }
    }
}
