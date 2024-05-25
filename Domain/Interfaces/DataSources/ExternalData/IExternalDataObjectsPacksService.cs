using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.ExternalData;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.DataSources.ExternalData
{
    public interface IExternalDataObjectsPacksService
    {
        public Task LoadExternalDataObject(AddExternalDataRequest request);

        public Task<List<ExternalObjectModelDTO>> LoadObjectsListFromFile(IFormFile file, string userId, string taskId);
        public Task<int?> LoadExternalDataObjectBackgroundJob(AddExternalDataWithoutFileRequest request, string userId, string taskId);

        public Task AddExternalDataObjectsToWorkspace(AddExternalDataObjectsPacksToWorkspaceRequest request);
        public Task AddExternalDataObjectsToWorkspaceBackgroundJob(AddExternalDataObjectsPacksToWorkspaceRequest request, string userId, string taskId);

        public Task LoadExternalDataAndAddToWorkspace(AddExternalDataRequest request);
        public Task LoadExternalDataAndAddToWorkspaceBackgroundJob(AddExternalDataWithoutFileRequest request, string userId, string groupTaskId, List<string> subTaskIds);


        public Task UpdatePack(UpdateExternalDataPackRequest request);

        public Task<ICollection<SimpleExternalObjectsPackDTO>> GetCollection(GetExternalDataObjectsPacksRequest request);
        public Task<ICollection<SimpleExternalObjectsPackDTO>> GetCustomerCollection(GetExternalDataObjectsPacksRequest request);

        public Task<FullExternalObjectsPackDTO> GetFullById(int id);
    }
}
