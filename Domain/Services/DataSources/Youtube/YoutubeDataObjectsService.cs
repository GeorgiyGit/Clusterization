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
using Domain.Resources.Types.Clusterization;
using Domain.Resources.Types.DataSources;
using Domain.DTOs;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Resources.Types.Tasks;

namespace Domain.Services.DataSources.Youtube
{
    public class YoutubeDataObjectsService:IYoutubeDataObjectsService
    {
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<YoutubeComment> _commentsRepository;
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _addPacksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;

        public YoutubeDataObjectsService(IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<YoutubeComment> commentsRepository,
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

        public async Task LoadCommentsByChannel(AddYoutubeCommentsToWorkspaceByChannelRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                YoutubeChannelId=request.ChannelId,
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingYoutubeCommentsToWorkspace],
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => LoadCommentsByChannelBackgroundJob(request, userId, taskId));
        }
        public async Task LoadCommentsByChannelBackgroundJob(AddYoutubeCommentsToWorkspaceByChannelRequest request, string userId, string taskId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();
                if (workspace == null || workspace.TypeId != ClusterizationTypes.YoutubeComments || workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], HttpStatusCode.BadRequest);
                
                if (workspace.IsProfilesInCalculation) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceProfilesInCalculation], HttpStatusCode.BadRequest);

                Expression<Func<Entities.DataSources.Youtube.YoutubeComment, bool>> filterCondition = e => e.ChannelId == request.ChannelId;

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
                    var comments = await _commentsRepository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(Entities.DataSources.Youtube.YoutubeComment.DataObject)}", pageParameters: new DTOs.PageParameters()
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
                                YoutubeComment = comment,
                                TypeId = DataObjectTypes.YoutubeComment,
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

                await _embeddingLoadingStatesService.ReviewStates(workspace.Id);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }

        public async Task LoadCommentsByVideos(AddYoutubeCommentsToWorkspaceByVideosRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingYoutubeCommentsToWorkspace],
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => LoadCommentsByVideosBackgroundJob(request, userId, taskId));
        }
        public async Task LoadCommentsByVideosBackgroundJob(AddYoutubeCommentsToWorkspaceByVideosRequest request, string userId, string taskId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();
                if (workspace == null || workspace.TypeId != ClusterizationTypes.YoutubeComments || workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], HttpStatusCode.BadRequest);

                if (workspace.IsProfilesInCalculation) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceProfilesInCalculation], HttpStatusCode.BadRequest);

                Expression<Func<Entities.DataSources.Youtube.YoutubeComment, bool>> filterCondition = e => true;

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

                    const int pageSize = 500;

                    var pageParameters = new PageParameters()
                    {
                        PageNumber = 1,
                        PageSize = pageSize
                    };
                    if (request.MaxCountInVideo < pageSize) pageParameters.PageSize = request.MaxCountInVideo;
                    
                    int loadedCount = 0;
                    while (true)
                    {
                        var comments = await _commentsRepository.GetAsync(filter: newConditions,
                                                                           includeProperties: $"{nameof(Entities.DataSources.Youtube.YoutubeComment.Video)},{nameof(Entities.DataSources.Youtube.YoutubeComment.DataObject)}",
                                                                           pageParameters: pageParameters);

                        if (comments.Count() == 0) break;

                        foreach (var comment in comments)
                        {
                            MyDataObject dataObject;
                            if (comment.DataObject == null)
                            {
                                dataObject = new MyDataObject()
                                {
                                    YoutubeComment = comment,
                                    TypeId = DataObjectTypes.YoutubeComment,
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
                                loadedCount++;
                            }
                        }

                        await _workspacesRepository.SaveChangesAsync();

                        float percents = (float)loadedCount / (float)request.MaxCountInVideo * 100f;
                        if (percents > 100f) percents = 100f;
                        await _tasksService.ChangeTaskPercent(taskId, percents);

                        if (loadedCount >= request.MaxCountInVideo) break;
                        
                        pageParameters.PageNumber++;
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
