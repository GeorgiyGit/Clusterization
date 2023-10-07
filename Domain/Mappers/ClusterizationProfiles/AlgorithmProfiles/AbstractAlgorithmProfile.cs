using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.Entities.Clusterization.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles.AlgorithmProfiles
{
    internal class AbstractAlgorithmProfile : AutoMapper.Profile
    {
        public AbstractAlgorithmProfile()
        {
            CreateMap<ClusterizationAbstactAlgorithm, AbstractAlgorithmDTO>()
                          .ForMember(dest => dest.TypeName,
                                     ost => ost.MapFrom(e => e.Type.Name));
        }
    }
}
