using AutoMapper;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.Entities.Quotas;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;

namespace Domain.Services.Quotas
{
    public class CustomerQuotasService : ICustomerQuotasService
    {
        private readonly IRepository<CustomerQuotas> _customerQuotasRepository;
        private readonly IRepository<QuotasPackItem> _packItemsRepositorty;
        
        private IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IQuotasLogsService _quotasLogsService;

        public CustomerQuotasService(IUserService userService,
            IRepository<CustomerQuotas> customerQuotasRepository,
            IRepository<QuotasPackItem> packItemsRepositorty,
            IMapper mapper,
            IQuotasLogsService quotasLogsService)
        {
            _userService = userService;
            _customerQuotasRepository = customerQuotasRepository;
            _packItemsRepositorty = packItemsRepositorty;
            _mapper = mapper;
            _quotasLogsService = quotasLogsService;
        }
        public async Task AddQuotasPackToCustomer(AddQuotasToCustomerRequest request)
        {
            var packItems = (await _packItemsRepositorty.GetAsync(e => e.PackId == request.PackId, includeProperties: $"{nameof(QuotasPackItem.Type)}"));

            foreach (var packItem in packItems)
            {
                var customerQuota = (await _customerQuotasRepository.GetAsync(e => e.CustomerId == request.CustomerId && e.TypeId == packItem.TypeId)).FirstOrDefault();

                if (customerQuota == null)
                {
                    var newQuota = new CustomerQuotas()
                    {
                        CustomerId = request.CustomerId,
                        TypeId = packItem.TypeId,
                        AvailableCount = packItem.Count,
                        ExpiredCount = 0
                    };

                    await _customerQuotasRepository.AddAsync(newQuota);
                    await _customerQuotasRepository.SaveChangesAsync();
                }
                else
                {
                    customerQuota.AvailableCount += packItem.Count;
                }
            }

            await _customerQuotasRepository.SaveChangesAsync();

            await _quotasLogsService.AddQuotasPackLogs(new AddQuotasPackLogsRequest()
            {
                CustomerId = request.CustomerId,
                PackId = request.PackId
            });
        }

        public async Task<ICollection<CustomerQuotasDTO>> GetAllCustomerQuotas()
        {
            var customerId = await _userService.GetCurrentUserId();

            var customerQuotasCollection = await _customerQuotasRepository.GetAsync(e => e.CustomerId == customerId, includeProperties:$"{nameof(CustomerQuotas.Type)}");

            return _mapper.Map<ICollection<CustomerQuotasDTO>>(customerQuotasCollection);
        }
    }
}
