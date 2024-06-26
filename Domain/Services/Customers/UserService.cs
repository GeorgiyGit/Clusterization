﻿using AutoMapper;
using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Customers
{
    public class UserService : IUserService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IRepository<Customer> _repository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        public UserService(IHttpContextAccessor httpContextAccessor,
            UserManager<Customer> userManager,
            IRepository<Customer> repository,
            IMapper mapper,
            IStringLocalizer<ErrorMessages> localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
            _localizer = localizer;
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

            var customers = (await _repository.GetAsync(filter: filterCondition, pageParameters: request.PageParameters)).ToList();

            var mappedUsers = _mapper.Map<List<SimpleCustomerDTO>>(customers);

            for(int i = 0; i < customers.Count(); i++)
            {
                mappedUsers[i].IsModerator = await _userManager.IsInRoleAsync(customers[i], UserRoles.Moderator);
            }

            return mappedUsers;
        }
        public async Task<string?> GetCustomerIdByEmail(string email)
        {
            var customer = (await _userManager.FindByEmailAsync(email));

            if (customer == null) return null;
            return customer.Id;
        }

        public async Task<FullCustomerInfoDTO> GetPersonalInfo()
        {
            var id = await GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotFound], HttpStatusCode.NotFound);

            var info = new FullCustomerInfoDTO()
            {
                CreationTime = user.CreationTime,
                Email = user.Email,
                UserName = user.UserName,
                IsEmailConfirmed = user.EmailConfirmed,
                IsModerator = await _userManager.IsInRoleAsync(user, UserRoles.Moderator),
                IsUser = await _userManager.IsInRoleAsync(user, UserRoles.User)
            };
            return info;
        }
    }
}
