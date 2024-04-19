using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasPacksService
    {
        public Task Add(AddQuotasPackRequest model);
        public Task<ICollection<QuotasPackDTO>> GetAll();
    }
}
