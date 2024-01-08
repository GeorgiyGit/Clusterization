using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class DBSCANProfile : AutoMapper.Profile
    {
        public DBSCANProfile()
        {
            CreateMap<DBScanAlgorithm, DBScanAlgorithmDTO>()
                .IncludeBase<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>()
                .ForMember(dest => dest.FullTitle,
                           ost => ost.MapFrom(e => e.Type.Name + " " + e.Epsilon + " " + e.MinimumPointsPerCluster));
        }
    }
}
