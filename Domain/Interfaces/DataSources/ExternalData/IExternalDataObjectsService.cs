using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using Domain.DTOs.ExternalData;

namespace Domain.Interfaces.DataSources.ExternalData
{
    public interface IExternalDataObjectsService
    {
        public Task<ICollection<SimpleExternalObjectDTO>> GetCollection(GetExternalDataObjectsRequest request);
    }
}
