using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class GaussianMixtureProfile : AutoMapper.Profile
    {
        public GaussianMixtureProfile()
        {
            CreateMap<GaussianMixtureAlgorithm, GaussianMixtureAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>()
                .ForMember(dest => dest.FullTitle,
                           ost => ost.MapFrom(e => e.Type.Name + " " + e.NumberOfComponents));
        }
    }
}
