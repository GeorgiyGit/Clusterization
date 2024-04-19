using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasTypesService
    {
        public Task<ICollection<QuotasTypeDTO>> GetAllTypes();
    }
}
