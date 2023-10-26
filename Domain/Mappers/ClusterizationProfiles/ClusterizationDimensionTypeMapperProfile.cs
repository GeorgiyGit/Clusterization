using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.Entities.Clusterization;

namespace Domain.Mappers.ClusterizationProfiles
{
    public class ClusterizationDimensionTypeMapperProfile:AutoMapper.Profile
    {
        public ClusterizationDimensionTypeMapperProfile()
        {
            CreateMap<ClusterizationDimensionType, ClusterizationDimensionTypeDTO>();
        }
    }
}
