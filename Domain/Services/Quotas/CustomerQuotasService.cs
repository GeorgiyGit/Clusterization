using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.Entities.Quotas;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Domain.Services.Quotas
{
    public class CustomerQuotasService : ICustomerQuotasService
    {
        private readonly IUserService _userService;
        private readonly IRepository<CustomerQuotas> _customerQuotesRepository;
        private readonly IRepository<QuotasPack> _packsRepositorty;
        private IStringLocalizer<ErrorMessages> _localizer;
        public CustomerQuotasService(IUserService userService,
            IRepository<CustomerQuotas> customerQuotesRepository,
            IRepository<QuotasPack> packsRepository,
            IStringLocalizer<ErrorMessages> localizer)
        {
            _userService = userService;
            _customerQuotesRepository = customerQuotesRepository;
            _packsRepositorty = packsRepository;
            _localizer = localizer;
        }
        public async Task AddQuotesPackToCustomer(AddQuotasToCustomerDTO request)
        {
            var pack = (await _packsRepositorty.GetAsync(e => e.Id == request.PackId, includeProperties: $"{nameof(QuotasPack.Items)}")).FirstOrDefault();

            if (pack == null) throw new HttpException(_localizer[ErrorMessagePatterns.QuotasPackNotFound], HttpStatusCode.NotFound);


        }

        public Task<ICollection<CustomerQuotasDTO>> GetAllCustomerQuotes()
        {
            throw new NotImplementedException();
        }
    }
}
