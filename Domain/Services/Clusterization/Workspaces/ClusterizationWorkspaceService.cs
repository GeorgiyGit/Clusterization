using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Interfaces.Clusterization.Workspaces;
using Domain.Interfaces.Other;
using Domain.Resources.Types.Clusterization;

namespace Domain.Services.Clusterization.Workspaces
{
    public class ClusterizationWorkspacesService : IClusterizationWorkspacesService
    {
        private readonly IRepository<ClusterizationWorkspace> _repository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public ClusterizationWorkspacesService(IRepository<ClusterizationWorkspace> repository,
                                               IStringLocalizer<ErrorMessages> localizer,
                                               IMapper mapper,
                                               IUserService userService,
                                               IQuotasControllerService quotasControllerService)
        {
            _repository = repository;
            _localizer = localizer;
            _mapper = mapper;
            _userService = userService;
            _quotasControllerService = quotasControllerService;
        }

        #region add-update
        public async Task Add(AddClusterizationWorkspaceRequest model)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            string type = null;

            if (model.VisibleType == VisibleTypes.AllCustomers)
            {
                type = QuotasTypes.PublicWorkspaces;
            }
            else if (model.VisibleType == VisibleTypes.OnlyOwner)
            {
                type = QuotasTypes.PrivateWorkspaces;
            }

            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, type, 1, Guid.NewGuid().ToString());

            if (!quotasResult)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }

            var newWorkspace = new ClusterizationWorkspace()
            {
                Title = model.Title,
                Description = model.Description,
                TypeId = model.TypeId,
                VisibleType = model.VisibleType,
                ChangingType = model.ChangingType,
                OwnerId = userId
            };

            await _repository.AddAsync(newWorkspace);
            await _repository.SaveChangesAsync();
        }
        public async Task Update(UpdateClusterizationWorkspaceRequest model)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var workspace = (await _repository.GetAsync(c => c.Id == model.Id, includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

            if (workspace == null || workspace.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            workspace.Title = model.Title;
            workspace.Description = model.Description;
            workspace.VisibleType = model.VisibleType;
            workspace.ChangingType = model.ChangingType;

            await _repository.SaveChangesAsync();
        }
        #endregion

        #region get
        public async Task<ClusterizationWorkspaceDTO> GetFullById(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            var workspace = (await _repository.GetAsync(c => c.Id == id && (c.VisibleType == VisibleTypes.AllCustomers || userId != null && c.OwnerId == userId), includeProperties: $"{nameof(ClusterizationWorkspace.Profiles)},{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<ClusterizationWorkspaceDTO>(workspace);
        }
        public async Task<SimpleClusterizationWorkspaceDTO> GetSimpleById(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            var workspace = (await _repository.GetAsync(c => c.Id == id && (c.VisibleType == VisibleTypes.AllCustomers || userId != null && c.OwnerId == userId), includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<SimpleClusterizationWorkspaceDTO>(workspace);
        }

        public async Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCollection(GetWorkspacesRequest request)
        {
            Expression<Func<ClusterizationWorkspace, bool>> filterCondition = e => true;


            if (request.FilterStr != null && request.FilterStr != "")
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => e.TypeId == request.TypeId && e.Title.Contains(request.FilterStr);
                }
                else
                {
                    filterCondition = e => e.Title.Contains(request.FilterStr);
                }
            }
            else
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => e.TypeId == request.TypeId;
                }
            }

            var userId = await _userService.GetCurrentUserId();

            if (userId != null)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers || e.OwnerId == userId);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }


            var workspaces = (await _repository.GetAsync(filterCondition, includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}", pageParameters: request.PageParameters));

            return _mapper.Map<ICollection<SimpleClusterizationWorkspaceDTO>>(workspaces);
        }
        public async Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCustomerCollection(GetWorkspacesRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            Expression<Func<ClusterizationWorkspace, bool>> filterCondition = e => e.OwnerId == userId;


            if (request.FilterStr != null && request.FilterStr != "")
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => e.TypeId == request.TypeId && e.Title.Contains(request.FilterStr);
                }
                else
                {
                    filterCondition = e => e.Title.Contains(request.FilterStr);
                }
            }
            else
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => e.TypeId == request.TypeId;
                }
            }

            if (userId != null)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers || e.OwnerId == userId);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }

            var workspaces = (await _repository.GetAsync(filterCondition, includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}", pageParameters: request.PageParameters));

            return _mapper.Map<ICollection<SimpleClusterizationWorkspaceDTO>>(workspaces);
        }

        public async Task<ICollection<string>> GetAllDataObjectsInList(int id)
        {
            List<string> stringList = new List<string>();

            var workspace = (await _repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)}")).FirstOrDefault();

            stringList = workspace.DataObjects.Select(e => e.Text).ToList();

            return stringList;
        }
        #endregion
    }
}
