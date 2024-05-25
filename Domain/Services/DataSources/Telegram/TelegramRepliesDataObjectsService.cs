using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.Youtube;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types.Clusterization;
using Domain.Resources.Types.DataSources;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using Domain.Entities.DataSources.Telegram;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using Domain.Extensions;
using Domain.Interfaces.DataSources.Telegram;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Resources.Types.Tasks;

namespace Domain.Services.DataSources.Telegram
{
    public class TelegramRepliesDataObjectsService: ITelegramRepliesDataObjectsService
    {
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<TelegramReply> _repliesRepository;
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _addPacksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;

        public TelegramRepliesDataObjectsService(IRepository<TelegramReply> repliesRepository,
            IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<MyDataObject> dataObjectsRepository,
            IRepository<WorkspaceDataObjectsAddPack> addPacksRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IStringLocalizer<TaskTitles> tasksLocalizer,
            IUserService userService,
            IBackgroundJobClient backgroundJobClient,
            IMyTasksService tasksService,
            IEmbeddingLoadingStatesService embeddingLoadingStatesService)
        {
            _repliesRepository = repliesRepository;
            _workspacesRepository = workspacesRepository;
            _dataObjectsRepository = dataObjectsRepository;
            _addPacksRepository = addPacksRepository;
            _localizer = localizer;
            _tasksLocalizer = tasksLocalizer;
            _userService = userService;
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
        }

        public async Task LoadRepliesByChannel(AddTelegramRepliesToWorkspaceByChannelRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                TelegramChannelId=request.ChannelId,
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingTelegramRepliesToWorkspace],
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => LoadRepliesByChannelBackgroundJob(request, userId, taskId));
        }
        public async Task LoadRepliesByChannelBackgroundJob(AddTelegramRepliesToWorkspaceByChannelRequest request, string userId, string taskId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();
                if (workspace == null || workspace.TypeId != ClusterizationTypes.TelegramReplies || workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], HttpStatusCode.BadRequest);

                if (workspace.IsProfilesInCalculation) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceProfilesInCalculation], HttpStatusCode.BadRequest);

                Expression<Func<TelegramReply, bool>> filterCondition = e => e.TelegramChannelId == request.ChannelId;

                if (request.DateFrom != null || request.DateTo != null)
                {
                    if (request.IsMessageDateCount)
                    {
                        if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.TelegramMessage.Date > request.DateFrom);
                        if (request.DateTo != null) filterCondition = filterCondition.And(e => e.TelegramMessage.Date < request.DateTo);
                    }
                    else
                    {
                        if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.Date > request.DateFrom);
                        if (request.DateTo != null) filterCondition = filterCondition.And(e => e.Date < request.DateTo);
                    }
                }

                var pack = new WorkspaceDataObjectsAddPack()
                {
                    OwnerId = userId,
                    WorkspaceId = workspace.Id
                };

                int addedElementsCount = 0;

                await _addPacksRepository.AddAsync(pack);
                await _addPacksRepository.SaveChangesAsync();

                await _embeddingLoadingStatesService.AddStatesToPack(pack.Id);

                int pageNumber = 1;
                int pageSize = 1000;
                while (true)
                {
                    var replies = await _repliesRepository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(TelegramReply.DataObject)}", pageParameters: new DTOs.PageParameters()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });

                    int count = 0;
                    foreach (var reply in replies)
                    {
                        MyDataObject dataObject;
                        if (reply.DataObject == null)
                        {
                            dataObject = new MyDataObject()
                            {
                                TelegramReply = reply,
                                TypeId = DataObjectTypes.TelegramReply,
                                Text = reply.Message,
                            };

                            await _dataObjectsRepository.AddAsync(dataObject);
                            await _dataObjectsRepository.SaveChangesAsync();
                        }
                        else
                        {
                            dataObject = reply.DataObject;
                            pack.DataObjects.Add(dataObject);
                        }

                        if (!workspace.DataObjects.Contains(dataObject))
                        {
                            pack.DataObjects.Add(dataObject);
                            dataObject.WorkspaceDataObjectsAddPacks.Add(pack);

                            workspace.DataObjects.Add(dataObject);
                            count++;
                            addedElementsCount++;
                            request.MaxCount--;
                            workspace.EntitiesCount++;
                        }
                        if (request.MaxCount <= 0) break;
                    }
                    await _workspacesRepository.SaveChangesAsync();

                    if (request.MaxCount <= 0) break;
                    if (replies.Count() < pageSize) break;
                    pageNumber++;
                }
                pack.DataObjectsCount = addedElementsCount;


                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);

                await _embeddingLoadingStatesService.ReviewStates(workspace.Id);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }

        public async Task LoadRepliesByMessages(AddTelegramRepliesToWorkspaceByMessagesRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingTelegramRepliesToWorkspace],
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => LoadRepliesByMessagesBackgroundJob(request, userId, taskId));
        }
        public async Task LoadRepliesByMessagesBackgroundJob(AddTelegramRepliesToWorkspaceByMessagesRequest request, string userId, string taskId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();
                if (workspace == null || workspace.TypeId != ClusterizationTypes.TelegramReplies || workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], HttpStatusCode.BadRequest);

                if (workspace.IsProfilesInCalculation) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceProfilesInCalculation], HttpStatusCode.BadRequest);

                Expression<Func<TelegramReply, bool>> filterCondition = e => true;

                if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.Date > request.DateFrom);
                if (request.DateTo != null) filterCondition = filterCondition.And(e => e.Date < request.DateTo);

                var pack = new WorkspaceDataObjectsAddPack()
                {
                    OwnerId = userId,
                    WorkspaceId = workspace.Id
                };

                int addedElementsCount = 0;

                await _addPacksRepository.AddAsync(pack);
                await _addPacksRepository.SaveChangesAsync();

                await _embeddingLoadingStatesService.AddStatesToPack(pack.Id);

                foreach (var id in request.MessageIds)
                {
                    var newConditions = filterCondition.And(e => e.TelegramMessageId == id);
                    var replies = (await _repliesRepository.GetAsync(filter: newConditions, includeProperties: $"{nameof(TelegramReply.TelegramMessage)},{nameof(TelegramReply.DataObject)}")).Take(request.MaxCountInMessage);

                    foreach (var reply in replies)
                    {
                        MyDataObject dataObject;
                        if (reply.DataObject == null)
                        {
                            dataObject = new MyDataObject()
                            {
                                TelegramReply = reply,
                                TypeId = DataObjectTypes.TelegramReply,
                                Text = reply.Message,
                            };

                            await _dataObjectsRepository.AddAsync(dataObject);
                            await _dataObjectsRepository.SaveChangesAsync();
                        }
                        else
                        {
                            dataObject = reply.DataObject;
                            pack.DataObjects.Add(dataObject);
                        }

                        if (!workspace.DataObjects.Contains(dataObject))
                        {
                            pack.DataObjects.Add(dataObject);
                            dataObject.WorkspaceDataObjectsAddPacks.Add(pack);

                            workspace.DataObjects.Add(dataObject);
                            workspace.EntitiesCount++;
                            addedElementsCount++;
                        }
                    }
                }

                pack.DataObjectsCount = addedElementsCount;

                await _workspacesRepository.SaveChangesAsync();

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);

                await _embeddingLoadingStatesService.ReviewStates(workspace.Id);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }
    }
}
