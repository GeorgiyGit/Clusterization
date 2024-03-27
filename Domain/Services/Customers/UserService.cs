using AutoMapper;
using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;
using Domain.Interfaces;
using Domain.Interfaces.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Customers
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Customer> _userManager;
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public UserService(IHttpContextAccessor httpContextAccessor,
            UserManager<Customer> userManager,
            IRepository<Customer> repository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string?> GetCurrentUserId()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (email == null) return null;
            var customer = (await _userManager.FindByNameAsync(email));

            if (customer == null) return null;
            return customer.Id;
        }

        public async Task<ICollection<SimpleCustomerDTO>> GetCustomers(GetCustomersRequest request)
        {
            Expression<Func<Customer, bool>> filterCondition = e => true;

            if (request.FilterStr != null && request.FilterStr != "") filterCondition = e => e.UserName.Contains(request.FilterStr) || e.Email.Contains(request.FilterStr);

            var customers = await _repository.GetAsync(filter: filterCondition, pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<SimpleCustomerDTO>>(customers);
        }
    }
}
