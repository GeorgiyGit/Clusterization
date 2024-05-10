using AutoMapper;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using Domain.DTOs.ExternalData;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Youtube;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DataSources.ExternalData;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types.Clusterization;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using TL;

namespace Domain.Services.DataSources.ExternalData
{
    public class ExternalDataObjectsService: IExternalDataObjectsService
    {
        private readonly IRepository<ExternalObject> _externalObjectsRepository;
        private readonly IRepository<ExternalObjectsPack> _externalObjectsPacksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ExternalDataObjectsService(IRepository<ExternalObjectsPack> externalObjectsPacksRepository,
            IRepository<ExternalObject> externalObjectsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IUserService userService,
            IMapper mapper)
        {
            _externalObjectsPacksRepository = externalObjectsPacksRepository;
            _externalObjectsRepository = externalObjectsRepository;
            _localizer = localizer;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ICollection<SimpleExternalObjectDTO>> GetCollection(GetExternalDataObjectsRequest request)
        {
            var userId = await _userService.GetCurrentUserId();

            var pack = await _externalObjectsPacksRepository.FindAsync(request.PackId);
            if (pack == null || (pack.VisibleType == VisibleTypes.OnlyOwner && (userId == null || userId != pack.OwnerId))) throw new HttpException(_localizer[ErrorMessagePatterns.ExternalObjectsPackVisibleTypeIsOnlyOwner], System.Net.HttpStatusCode.BadRequest);
            
            var objects = await _externalObjectsRepository.GetAsync(e=>e.PackId==request.PackId, pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<SimpleExternalObjectDTO>>(objects);
        }
    }
}
