
namespace Domain.Interfaces.Quotas
{
    public interface IQuotasControllerService
    {
        public Task<int> GetCustomerQuotasCount(string customerId, string typeId);
        public Task<bool> TakeCustomerQuotas(string customerId, string typeId, int quotasCount, string logsId);
    }
}
