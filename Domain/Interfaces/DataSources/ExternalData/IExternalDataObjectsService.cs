using Domain.DTOs.ExternalData;

namespace Domain.Interfaces.DataSources.ExternalData
{
    public interface IExternalDataObjectsService
    {
        public Task LoadExternalData(AddExternalDataRequest data);
    }
}
