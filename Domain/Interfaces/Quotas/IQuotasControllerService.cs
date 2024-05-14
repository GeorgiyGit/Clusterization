
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using System.Net;

namespace Domain.Interfaces.Quotas
{
    public interface IQuotasControllerService
    {
        public Task<int> GetCustomerQuotasCount(string customerId, string typeId);
        public Task<bool> TakeCustomerQuotas(string customerId, string typeId, int quotasCount, string logsId);
        
        public Task AddAvailableCount(int count, string type, string userId);
        public Task RemoveExpireCount(int count, string type, string userId);
        public Task<bool> AddCustomerQuotas(string customerId, string typeId, int quotasCount, string logsId);
    }
}
