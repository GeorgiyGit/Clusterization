using AutoMapper;
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
        private readonly IRepository<CustomerQuotas> _customerQuotasRepository;
        private readonly IRepository<QuotasPackItem> _packItemsRepositorty;
        private IStringLocalizer<ErrorMessages> _localizer;
        private IMapper _mapper;
        public CustomerQuotasService(IUserService userService,
            IRepository<CustomerQuotas> customerQuotasRepository,
            IRepository<QuotasPackItem> packItemsRepositorty,
            IStringLocalizer<ErrorMessages> localizer,
            IMapper mapper)
        {
            _userService = userService;
            _customerQuotasRepository = customerQuotasRepository;
            _packItemsRepositorty = packItemsRepositorty;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task AddQuotasPackToCustomer(AddQuotasToCustomerDTO request)
        {
            var packItems = (await _packItemsRepositorty.GetAsync(e => e.PackId == request.PackId, includeProperties: $"{nameof(QuotasPackItem.Type)}"));

            foreach (var packItem in packItems)
            {
                var customerQuota = (await _customerQuotasRepository.GetAsync(e => e.CustomerId == request.CustomerId && e.TypeId == packItem.TypeId)).FirstOrDefault();

                if (customerQuota == null) throw new HttpException(_localizer[ErrorMessagePatterns.QuotasPackNotFound], HttpStatusCode.NotFound);
                
                customerQuota.AvailableCount += packItem.Count;
            }

            await _customerQuotasRepository.SaveChangesAsync();
        }

        public async Task<ICollection<CustomerQuotasDTO>> GetAllCustomerQuotas()
        {
            var customerId = await _userService.GetCurrentUserId();

            var customerQuotasCollection = await _customerQuotasRepository.GetAsync(e => e.CustomerId == customerId);

            return _mapper.Map<ICollection<CustomerQuotasDTO>>(customerQuotasCollection);
        }
    }
}
