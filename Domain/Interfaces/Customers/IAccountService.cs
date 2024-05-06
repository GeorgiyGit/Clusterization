using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Domain.Services.Customers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Customers
{
    public interface IAccountService
    {
        public Task<TokenDTO> SignUp(CustomerSignUpRequest request);
        public Task<TokenDTO> LogIn(CustomerLogInRequest request);

        public Task<TokenDTO> ConfirmEmail(string token, string email);
        public Task<bool> CheckEmailConfirmation();
        public Task SendEmailConfirmation();
    }
}
