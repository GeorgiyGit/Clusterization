using Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Respones;

namespace Domain.Interfaces.Clusterization.Workspaces
{
    public interface IWorkspaceDataObjectsAddPacksService
    {
        public Task<WorkspaceDataObjectsAddPackSimpleDTO> GetSimplePackById(int id);
        public Task<ICollection<WorkspaceDataObjectsAddPackSimpleDTO>> GetSimplePacks(GetWorkspaceDataObjectsAddPacksRequest request);
        public Task<WorkspaceDataObjectsAddPackFullDTO> GetFullPack(int id);

        public Task<ICollection<WorkspaceDataObjectsAddPackSimpleDTO>> GetCustomerSimplePacks(GetCustomerWorkspaceDataObjectsAddPacksRequest request);

        public Task DeletePack(int id);
        public Task RestorePack(int id);
    }
}
