using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasTypesService
    {
        public Task<ICollection<QuotasTypeDTO>> GetAllTypes();
    }
}
