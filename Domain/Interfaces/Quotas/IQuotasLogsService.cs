using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasLogsService
    {
        public Task<ICollection<QuotasLogsDTO>> GetQuotasLogs(GetQuotasLogsRequest request);
        public Task<ICollection<QuotasPackLogsDTO>> GetQuotasPackLogs(GetQuotasPackLogsRequest request);

        public Task AddQuotasLogs(AddQuotasLogsRequest model);
        public Task AddQuotasPackLogs(AddQuotasPackLogsRequest model);
    }
}
