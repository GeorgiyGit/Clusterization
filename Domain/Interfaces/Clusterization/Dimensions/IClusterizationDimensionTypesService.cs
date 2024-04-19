using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;

namespace Domain.Interfaces.Clusterization.Dimensions
{
    public interface IClusterizationDimensionTypesService
    {
        public Task<ICollection<ClusterizationDimensionTypeDTO>> GetAll();
    }
}
