using AutoMapper;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses;
using Domain.Entities.Quotas;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Quotas
{
    public class QuotasLogsService : IQuotasLogsService
    {
        private readonly IRepository<QuotasLogs> _quotasLogsRepository;
        private readonly IRepository<QuotasPackLogs> _quotasPackLogsRepository;
        private readonly IRepository<QuotasPack> _packRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        public QuotasLogsService(IRepository<QuotasLogs> quotasLogsRepository,
                                 IRepository<QuotasPackLogs> quotasPackLogsRepository,
                                 IRepository<QuotasPack> packRepository,
                                 IMapper mapper,
                                 IUserService userService,
                                 IStringLocalizer<ErrorMessages> localizer)
        {
            _quotasLogsRepository = quotasLogsRepository;
            _quotasPackLogsRepository = quotasPackLogsRepository;
            _packRepository = packRepository;
            _mapper = mapper;
            _userService = userService;
            _localizer = localizer;
        }

        public async Task<ICollection<QuotasLogsDTO>> GetQuotasLogs(GetQuotasLogsRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            Expression<Func<QuotasLogs, bool>> filterCondition = e => e.CustomerId == userId;

            if (request.TypeId != null)
            {
                filterCondition = filterCondition.And(e => e.TypeId == request.TypeId);
            }
            var logs = (await _quotasLogsRepository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(QuotasLogs.Type)},{nameof(QuotasLogs.Customer)}"));

            return _mapper.Map<ICollection<QuotasLogsDTO>>(logs);
        }

        public async Task<ICollection<QuotasPackLogsDTO>> GetQuotasPackLogs(GetQuotasPackLogsRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var logsCollection = await _quotasPackLogsRepository.GetAsync(e => e.CustomerId == userId);

            var mappedLogs = _mapper.Map<ICollection<QuotasPackLogsDTO>>(logsCollection);

            foreach(var logs in mappedLogs)
            {
                var pack = (await _packRepository.GetAsync(e => e.Id == logs.Id, includeProperties: $"{nameof(QuotasPack.Items)}")).FirstOrDefault();

                logs.Pack = _mapper.Map<QuotasPackDTO>(pack);
            }

            return mappedLogs;
        }
    }
}
