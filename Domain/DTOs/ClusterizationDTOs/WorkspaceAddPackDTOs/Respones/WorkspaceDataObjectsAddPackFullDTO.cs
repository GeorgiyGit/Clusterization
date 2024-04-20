using Domain.DTOs.CustomerDTOs.Responses;
using Domain.DTOs.EmbeddingDTOs;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Respones
{
    public class WorkspaceDataObjectsAddPackFullDTO
    {
        public int Id { get; set; }

        public int DataObjectsCount { get; set; }

        public SimpleCustomerDTO Owner { get; set; }

        public ICollection<EmbeddingLoadingStateDTO> EmbeddingLoadingStates { get; set; }

        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
