using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;

namespace Domain.Interfaces.Quotas
{
    public interface ICustomerQuotasService
    {
        public Task AddQuotesPackToCustomer(AddQuotasToCustomerDTO request);
        public Task<ICollection<CustomerQuotasDTO>> GetAllCustomerQuotas();
    }
}
