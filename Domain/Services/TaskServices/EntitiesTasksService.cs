using AutoMapper;
using Domain.DTOs.TaskDTOs.Responses;
using Domain.DTOs.TaskDTOs;
using Domain.Entities.Tasks;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.TaskDTOs.GetEntitiesTaskDTOs;
using Domain.Extensions;
using Domain.Entities.Clusterization.Profiles;
using Domain.Exceptions;
using Domain.Resources.Types.Clusterization;
using Domain.Entities.Clusterization.Workspaces;
using Domain.DTOs;
using Domain.Entities.DataSources.ExternalData;
using Domain.Interfaces.Tasks;
using System.Net;

namespace Domain.Services.TaskServices
{
    public class EntitiesTasksService: IEntitiesTasksService
    {
        private readonly IRepository<MyMainTask> _mainTasksRepository;
        private readonly IRepository<MySubTask> _subTasksRepository;
        private readonly IRepository<ClusterizationProfile> _profilesRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<ExternalObjectsPack> _externalObjectsPacksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EntitiesTasksService(IRepository<ClusterizationProfile> profilesRepository,
            IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<ExternalObjectsPack> externalObjectsPacksRepository,
            IRepository<MyMainTask> mainTasksRepository,
            IRepository<MySubTask> subTasksRepository,
            IMapper mapper,
            IStringLocalizer<ErrorMessages> localizer,
            IUserService userService)
        {
            _mainTasksRepository = mainTasksRepository;
            _profilesRepository = profilesRepository;
            _workspacesRepository = workspacesRepository;
            _externalObjectsPacksRepository = externalObjectsPacksRepository;
            _subTasksRepository = subTasksRepository;
            _mapper = mapper;
            _localizer = localizer;
            _userService = userService;
        }
        public async Task<ICollection<MainTaskDTO>> GetProfileTasks(GetEntityTasksRequest<int> request)
        {
            var profile = await _profilesRepository.FindAsync(request.Id);
            if (profile == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);
            if (profile.VisibleType == VisibleTypes.OnlyOwner)
            {
                var customerId = await _userService.GetCurrentUserId();
                if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (customerId != profile.OwnerId) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);
            }

            Expression<Func<MyMainTask, bool>> filterCondition = e => e.ClusterizationProfileId == profile.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }
        public async Task<ICollection<MainTaskDTO>> GetWorkspaceTasks(GetEntityTasksRequest<int> request)
        {
            var workspace = await _workspacesRepository.FindAsync(request.Id);
            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);
            if (workspace.VisibleType == VisibleTypes.OnlyOwner)
            {
                var customerId = await _userService.GetCurrentUserId();
                if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (customerId != workspace.OwnerId) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);
            }

            Expression<Func<MyMainTask, bool>> filterCondition = e => e.ClusterizationProfileId == workspace.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }

        public async Task<ICollection<MainTaskDTO>> GetYoutubeChannelTasks(GetEntityTasksRequest<string> request)
        {
            Expression<Func<MyMainTask, bool>> filterCondition = e => e.YoutubeChannelId == request.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }
        public async Task<ICollection<MainTaskDTO>> GetYoutubeVideoTasks(GetEntityTasksRequest<string> request)
        {
            Expression<Func<MyMainTask, bool>> filterCondition = e => e.YoutubeVideoId == request.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }

        public async Task<ICollection<MainTaskDTO>> GetTelegramChannelTasks(GetEntityTasksRequest<long> request)
        {
            Expression<Func<MyMainTask, bool>> filterCondition = e => e.TelegramChannelId == request.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }
        public async Task<ICollection<MainTaskDTO>> GetTelegramMessageTasks(GetEntityTasksRequest<long> request)
        {
            Expression<Func<MyMainTask, bool>> filterCondition = e => e.TelegramMessageId == request.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }

        public async Task<ICollection<MainTaskDTO>> GetExternalObjectsPackTasks(GetEntityTasksRequest<int> request)
        {
            var externalObjectsPack = await _externalObjectsPacksRepository.FindAsync(request.Id);
            if (externalObjectsPack == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);
            if (externalObjectsPack.VisibleType == VisibleTypes.OnlyOwner)
            {
                var customerId = await _userService.GetCurrentUserId();
                if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (customerId != externalObjectsPack.OwnerId) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);
            }
            Expression<Func<MyMainTask, bool>> filterCondition = e => e.ExternalObjectsPackId == externalObjectsPack.Id;
            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            return await GetTasksWithFilterCondition(filterCondition, request.PageParameters);
        }

        public async Task<ICollection<MainTaskDTO>> GetTasksWithFilterCondition(Expression<Func<MyMainTask, bool>> filterCondition, PageParameters pageParameters)
        {
            var tasks = (await _mainTasksRepository.GetAsync(filter: filterCondition,
                                                             orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                             includeProperties: $"{nameof(MyBaseTask.State)}",
                                                             pageParameters: pageParameters)).ToList();
            
            var mappedTasks = new List<MainTaskDTO>(tasks.Count);

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];

                var subTasks = await _subTasksRepository.GetAsync(e => e.GroupTaskId == task.Id,
                                                                  order => order.OrderBy(e => e.Position),
                                                                  includeProperties: $"{nameof(MyBaseTask.State)}");

                var mappedSubTasks = _mapper.Map<ICollection<SimpleSubTaskDTO>>(subTasks);

                var mappedTask = _mapper.Map<MainTaskDTO>(task);
                mappedTask.SubTasks = mappedSubTasks;
                mappedTasks.Add(mappedTask);
            }

            return mappedTasks;
        }
    }
}
