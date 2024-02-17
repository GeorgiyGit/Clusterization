using Domain.Entities.Customers;
using Domain.Interfaces.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Customers
{
    public class UserService:IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Customer> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor,
            UserManager<Customer> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<string?> GetCurrentUserId()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (email == null) return null;
            var customer = (await _userManager.FindByNameAsync(email));

            if(customer == null) return null;
            return customer.Id;
        }
    }
}
