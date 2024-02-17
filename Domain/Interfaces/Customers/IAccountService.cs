using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Customers
{
    public interface IAccountService
    {
        public Task<TokenDTO> SignUp(CustomerSignUpRequest request);

        public Task<TokenDTO> LogIn(CustomerLogInRequest request);
    }
}
