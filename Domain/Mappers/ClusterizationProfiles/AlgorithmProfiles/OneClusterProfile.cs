using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class OneClusterProfile : AutoMapper.Profile
    {
        public OneClusterProfile()
        {
            CreateMap<OneClusterAlgorithm, OneClusterAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>();
        }
    }
}
