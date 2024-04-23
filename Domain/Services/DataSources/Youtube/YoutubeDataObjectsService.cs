using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class YoutubeDataObjectsService:IYoutubeDataObjectsService
    {
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<Comment> _commentsRepository;
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _addPacksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;

        public YoutubeDataObjectsService(IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<Comment> commentsRepository,
            IRepository<MyDataObject> dataObjectsRepository,
            IRepository<WorkspaceDataObjectsAddPack> addPacksRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IStringLocalizer<TaskTitles> tasksLocalizer,
            IUserService userService,
            IBackgroundJobClient backgroundJobClient,
            IMyTasksService tasksService,
            IEmbeddingLoadingStatesService embeddingLoadingStatesService)
        {
            _workspacesRepository = workspacesRepository;
            _commentsRepository = commentsRepository;
            _dataObjectsRepository = dataObjectsRepository;
            _addPacksRepository = addPacksRepository;
            _localizer = localizer;
            _tasksLocalizer = tasksLocalizer;
            _userService = userService;
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
        }

        public async Task LoadCommentsByChannel(AddCommentsToWorkspaceByChannelRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.AddingCommentsToWorkspace]);

            _backgroundJobClient.Enqueue(() => LoadCommentsByChannelBackgroundJob(request, userId, taskId));
        }
        public async Task LoadCommentsByChannelBackgroundJob(AddCommentsToWorkspaceByChannelRequest request, string userId, int taskId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

                if (workspace == null || workspace.TypeId != ClusterizationTypes.Comments || workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

                Expression<Func<Entities.DataSources.Youtube.Comment, bool>> filterCondition = e => e.ChannelId == request.ChannelId;

                if (request.DateFrom != null || request.DateTo != null)
                {
                    if (request.IsVideoDateCount)
                    {
                        if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.Video.PublishedAtDateTimeOffset > request.DateFrom);
                        if (request.DateTo != null) filterCondition = filterCondition.And(e => e.Video.PublishedAtDateTimeOffset < request.DateTo);
                    }
                    else
                    {
                        if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset > request.DateFrom);
                        if (request.DateTo != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset < request.DateTo);
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
                    var comments = await _commentsRepository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(Entities.DataSources.Youtube.Comment.DataObject)}", pageParameters: new DTOs.PageParameters()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });

                    int count = 0;
                    foreach (var comment in comments)
                    {
                        MyDataObject dataObject;
                        if (comment.DataObject == null)
                        {
                            dataObject = new MyDataObject()
                            {
                                Comment = comment,
                                TypeId = DataObjectTypes.Comment,
                                Text = comment.TextOriginal,
                            };

                            await _dataObjectsRepository.AddAsync(dataObject);
                            await _dataObjectsRepository.SaveChangesAsync();
                        }
                        else
                        {
                            dataObject = comment.DataObject;
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
                    if (comments.Count() < pageSize) break;
                    pageNumber++;
                }
                pack.DataObjectsCount = addedElementsCount;


                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }

        public async Task LoadCommentsByVideos(AddCommentsToWorkspaceByVideosRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.AddingCommentsToWorkspace]);

            _backgroundJobClient.Enqueue(() => LoadCommentsByVideosBackgroundJob(request, userId, taskId));
        }
        public async Task LoadCommentsByVideosBackgroundJob(AddCommentsToWorkspaceByVideosRequest request, string userId, int taskId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();

                if (workspace == null || workspace.TypeId != ClusterizationTypes.Comments || workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);
                Expression<Func<Entities.DataSources.Youtube.Comment, bool>> filterCondition = e => true;

                if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset > request.DateFrom);
                if (request.DateTo != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset < request.DateTo);

                var pack = new WorkspaceDataObjectsAddPack()
                {
                    OwnerId = userId,
                    WorkspaceId = workspace.Id
                };

                int addedElementsCount = 0;

                await _addPacksRepository.AddAsync(pack);
                await _addPacksRepository.SaveChangesAsync();

                await _embeddingLoadingStatesService.AddStatesToPack(pack.Id);

                foreach (var id in request.VideoIds)
                {
                    var newConditions = filterCondition.And(e => e.VideoId == id);
                    var comments = (await _commentsRepository.GetAsync(filter: newConditions, includeProperties: $"{nameof(Entities.DataSources.Youtube.Comment.Video)},{nameof(Entities.DataSources.Youtube.Comment.DataObject)}")).Take(request.MaxCountInVideo);

                    foreach (var comment in comments)
                    {
                        MyDataObject dataObject;
                        if (comment.DataObject == null)
                        {
                            dataObject = new MyDataObject()
                            {
                                Comment = comment,
                                TypeId = DataObjectTypes.Comment,
                                Text = comment.TextOriginal,
                            };

                            await _dataObjectsRepository.AddAsync(dataObject);
                            await _dataObjectsRepository.SaveChangesAsync();
                        }
                        else
                        {
                            dataObject = comment.DataObject;
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
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }
    }
}
