using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasControllerService
    {
        public Task<int> GetCustomerQuotasCount(string customerId, string typeId);
        public Task<bool> TakeCustomerQuotas(string customerId, string typeId, int quotasCount);
    }
}
