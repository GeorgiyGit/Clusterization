using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.Entities.Quotas;
using Domain.Exceptions;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Quotas
{
    public class QuotasControllerService : IQuotasControllerService
    {
        private readonly IRepository<CustomerQuotas> _customerQuotasRepository;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IQuotasLogsService _quotasLogsService;
        public QuotasControllerService(IRepository<CustomerQuotas> customerQuotasRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IQuotasLogsService quotasLogsService)
        {
            _customerQuotasRepository = customerQuotasRepository;
            _localizer = localizer;
            _quotasLogsService = quotasLogsService;
        }
        public async Task<int> GetCustomerQuotasCount(string customerId, string typeId)
        {
            var customerQuotas = (await _customerQuotasRepository.GetAsync(e => e.CustomerId == customerId && e.TypeId == typeId)).FirstOrDefault();

            if (customerQuotas == null) throw new HttpException(_localizer[ErrorMessagePatterns.CustomerQuotasNotFound], HttpStatusCode.NotFound);

            return customerQuotas.AvailableCount;
        }

        public async Task<bool> TakeCustomerQuotas(string customerId, string typeId, int quotasCount, string logsId)
        {
            var customerQuotas = (await _customerQuotasRepository.GetAsync(e => e.CustomerId == customerId && e.TypeId == typeId)).FirstOrDefault();

            if (customerQuotas == null) throw new HttpException(_localizer[ErrorMessagePatterns.CustomerQuotasNotFound], HttpStatusCode.NotFound);

            if (customerQuotas.AvailableCount - quotasCount < 0) return false;

            try
            {
                customerQuotas.AvailableCount -= quotasCount;
                customerQuotas.ExpiredCount += quotasCount;

                await _customerQuotasRepository.SaveChangesAsync();

                await _quotasLogsService.AddQuotasLogs(new AddQuotasLogsRequest()
                {
                    Id = logsId,
                    Count = quotasCount,
                    CustomerId = customerId,
                    TypeId = typeId
                });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
