using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings.DimensionEntities;

namespace Domain.Mappers.ClusterizationProfiles
{
    public class ClusterizationDimensionTypeMapperProfile:AutoMapper.Profile
    {
        public ClusterizationDimensionTypeMapperProfile()
        {
            CreateMap<DimensionType, ClusterizationDimensionTypeDTO>();
        }
    }
}
