using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entitie.Clusterization.Algorithms.Non_hierarchical;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class DBSCANProfile : AutoMapper.Profile
    {
        public DBSCANProfile()
        {
            CreateMap<DBSCANAlgorithm, DBSCANAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>()
                .ForMember(dest => dest.FullTitle,
                           ost => ost.MapFrom(e => e.Type.Name + " " + e.Epsilon + " " + e.MinimumPointsPerCluster));
        }
    }
}
