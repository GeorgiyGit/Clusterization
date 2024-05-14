using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using System.Net;

namespace Domain.Interfaces.Quotas
{
    public interface ICustomerQuotasService
    {
        public Task AddQuotasPackToCustomer(AddQuotasToCustomerRequest request);
        public Task<ICollection<CustomerQuotasDTO>> GetAllCustomerQuotas();
    }
}
