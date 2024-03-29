using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Customers
{
    public interface IUserService
    {
        internal Task<string?> GetCurrentUserId();
        public Task<ICollection<SimpleCustomerDTO>> GetCustomers(GetCustomersRequest request);
        public Task<string?> GetCustomerIdByEmail(string email);
    }
}
