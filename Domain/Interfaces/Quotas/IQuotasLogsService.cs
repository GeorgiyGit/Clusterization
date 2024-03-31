using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasLogsService
    {
        public Task<ICollection<QuotasLogsDTO>> GetQuotasLogs(GetQuotasLogsRequest request);
        public Task<ICollection<QuotasPackLogsDTO>> GetQuotasPackLogs(GetQuotasPackLogsRequest request);

        public Task AddQuotasLogs(AddQuotasLogsDTO model);
        public Task AddQuotasPackLogs(AddQuotasPackLogsDTO model);
    }
}
