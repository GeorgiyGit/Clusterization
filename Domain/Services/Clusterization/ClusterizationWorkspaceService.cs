using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.DTOs.ExternalData;
using Domain.Entities.Clusterization;
using Domain.Entities.ExternalData;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;

namespace Domain.Services.Clusterization
{
    public class ClusterizationWorkspacesService : IClusterizationWorkspacesService
    {
        private readonly IRepository<ClusterizationWorkspace> repository;
        private readonly IRepository<Entities.Youtube.Comment> comments_repository;
        private readonly IRepository<ExternalObject> externalObjects_repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;
        private readonly IUserService _userService;
        public ClusterizationWorkspacesService(IRepository<ClusterizationWorkspace> repository,
                                               IStringLocalizer<ErrorMessages> localizer,
                                               IMapper mapper,
                                               IRepository<Entities.Youtube.Comment> comments_repository,
                                               IRepository<ExternalObject> externalObjects_repository,
                                               IBackgroundJobClient backgroundJobClient,
                                               IMyTasksService taskService,
                                               IUserService userService)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.comments_repository = comments_repository;
            this.backgroundJobClient = backgroundJobClient;
            this.taskService = taskService;
            this.externalObjects_repository = externalObjects_repository;
            _userService = userService;
        }

        #region add-update
        public async Task Add(AddClusterizationWorkspaceDTO model)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var newWorkspace = new ClusterizationWorkspace()
            {
                Title = model.Title,
                Description = model.Description,
                TypeId = model.TypeId,
                VisibleType = model.VisibleType,
                ChangingType = model.ChangingType,
                OwnerId = userId
            };

            await repository.AddAsync(newWorkspace);
            await repository.SaveChangesAsync();
        }
        public async Task Update(UpdateClusterizationWorkspaceDTO model)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var workspace = (await repository.GetAsync(c => c.Id == model.Id, includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

            if (workspace == null || workspace.OwnerId!=userId) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            workspace.Title = model.Title;
            workspace.Description = model.Description;
            workspace.VisibleType = model.VisibleType;
            workspace.ChangingType = model.ChangingType;

            await repository.SaveChangesAsync();
        }
        #endregion

        #region get
        public async Task<ClusterizationWorkspaceDTO> GetFullById(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            var workspace = (await repository.GetAsync(c => c.Id == id && (c.VisibleType == VisibleTypes.AllCustomers || (userId != null && c.OwnerId == userId)), includeProperties: $"{nameof(ClusterizationWorkspace.Profiles)},{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            return mapper.Map<ClusterizationWorkspaceDTO>(workspace);
        }
        public async Task<SimpleClusterizationWorkspaceDTO> GetSimpleById(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            var workspace = (await repository.GetAsync(c => c.Id == id && (c.VisibleType == VisibleTypes.AllCustomers || (userId != null && c.OwnerId == userId)), includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            return mapper.Map<SimpleClusterizationWorkspaceDTO>(workspace);
        }
        public async Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCollection(GetWorkspacesRequest request)
        {
            Expression<Func<ClusterizationWorkspace, bool>> filterCondition = e => true;


            if (request.FilterStr != null && request.FilterStr!="")
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => (e.TypeId == request.TypeId) && e.Title.Contains(request.FilterStr);
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
                    filterCondition = e => (e.TypeId == request.TypeId);
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
            

            var pageParameters = request.PageParameters;
            var workspaces = (await repository.GetAsync(filterCondition,includeProperties:$"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}"))
                                              .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                              .Take(pageParameters.PageSize).ToList();

            return mapper.Map<ICollection<SimpleClusterizationWorkspaceDTO>>(workspaces);
        }
        #endregion

        #region add-entity
        public async Task LoadCommentsByChannel(AddCommentsToWorkspaceByChannelRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            backgroundJobClient.Enqueue(() => LoadCommentsByChannelBackgroundJob(request, userId));
        }
        public async Task LoadCommentsByChannelBackgroundJob(AddCommentsToWorkspaceByChannelRequest request, string userId)
        {
            var taskId = await taskService.CreateTask("Додавання коментарів");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await repository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)},{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}")).FirstOrDefault();

                if (workspace == null || workspace.TypeId != ClusterizationTypes.Comments ||(workspace.ChangingType==ChangingTypes.OnlyOwner && (userId==null || userId!=workspace.OwnerId))) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

                Expression<Func<Entities.Youtube.Comment, bool>> filterCondition = e => e.ChannelId == request.ChannelId;

                if (request.DateFrom != null || request.DateTo != null)
                {
                    if(request.IsVideoDateCount)
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

                int pageNumber = 1;
                int pageSize = 1000;
                while (true){
                    var comments = (await comments_repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(Entities.Youtube.Comment.Workspaces)}", pageParameters: new DTOs.PageParametersDTO()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    }));

                    int count = 0;
                    foreach (var comment in comments)
                    {
                        if (!workspace.Comments.Contains(comment))
                        {
                            workspace.Comments.Add(comment);
                            comment.Workspaces.Add(workspace);
                            count++;
                            request.MaxCount--;
                            workspace.EntitiesCount++;
                        }
                        if (request.MaxCount <= 0) break;
                    }
                    await repository.SaveChangesAsync();

                    if (request.MaxCount <= 0) break;
                    if (comments.Count() < pageSize) break;
                    pageNumber++;
                }
                workspace.IsAllDataEmbedded = false;


                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
        }

        public async Task LoadCommentsByVideos(AddCommentsToWorkspaceByVideosRequest request)
        {
            backgroundJobClient.Enqueue(() => LoadCommentsByVideosBackgroundJob(request));
        }
        public async Task LoadCommentsByVideosBackgroundJob(AddCommentsToWorkspaceByVideosRequest request)
        {
            var taskId = await taskService.CreateTask("Додавання коментарів");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            var userId = await _userService.GetCurrentUserId();
            try
            {
                var workspace = (await repository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();

                if (workspace == null || workspace.TypeId != ClusterizationTypes.Comments || (workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId))) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);
                Expression<Func<Entities.Youtube.Comment, bool>> filterCondition = e => true;

                if (request.DateFrom != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset > request.DateFrom);
                if (request.DateTo != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset < request.DateTo);

                foreach (var id in request.VideoIds)
                {
                    var newConditions = filterCondition.And(e => e.VideoId == id);
                    var comments = (await comments_repository.GetAsync(filter: newConditions, includeProperties: $"{nameof(Entities.Youtube.Comment.Video)},{nameof(Entities.Youtube.Comment.Workspaces)}")).Take(request.MaxCountInVideo);

                    foreach (var comment in comments)
                    {
                        if (!workspace.Comments.Contains(comment))
                        {
                            workspace.Comments.Add(comment);
                            comment.Workspaces.Add(workspace);
                            workspace.EntitiesCount++;
                        }
                    }
                }

                workspace.IsAllDataEmbedded = false;

                await repository.SaveChangesAsync();

                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
        }

        public async Task LoadExternalData(AddExternalDataDTO data)
        {
            var workspace = (await repository.GetAsync(c => c.Id == data.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.ExternalObjects)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();

            var userId = await _userService.GetCurrentUserId();
            if (workspace == null || workspace.TypeId != ClusterizationTypes.External || (workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId))) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            var taskId = await taskService.CreateTask("Додавання зовнішніх об'єктів");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);
            try
            {
                var file = data.File; // Assuming a single file is being uploaded

                if (file != null && file.Length > 0)
                {
                    var fileContent = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd(); // Read the text content of the file
                    }
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalDataFileModelDTO>(fileContent);

                    if (responseObject == null) throw new HttpException(localizer[ErrorMessagePatterns.FileError], HttpStatusCode.BadRequest);

                    var oldExternalObjects = await externalObjects_repository.GetAsync(e => e.Session == responseObject.Session && e.Workspaces.Contains(workspace));

                    if (oldExternalObjects.Count() > 0)
                    {
                        foreach (var newExtObj in responseObject.ExternalObjects)
                        {
                            var extObj = oldExternalObjects.FirstOrDefault(e => e.Id == newExtObj.Id);

                            if (extObj != null)
                            {
                                extObj.Text = newExtObj.Text;
                            }
                            else
                            {
                                var extObjectForAdding = new ExternalObject()
                                {
                                    Text = newExtObj.Text,
                                    Session = responseObject.Session,
                                    Id = newExtObj.Id,
                                    FullId = workspace.Id+ responseObject.Session + newExtObj.Id
                                };
                                await externalObjects_repository.AddAsync(extObjectForAdding);

                                workspace.ExternalObjects.Add(extObjectForAdding);
                                extObjectForAdding.Workspaces.Add(workspace);

                                workspace.EntitiesCount++;
                            }
                        }
                    }
                    else
                    {
                        foreach (var newExtObj in responseObject.ExternalObjects)
                        {
                            var extObjectForAdding = new ExternalObject()
                            {
                                Text = newExtObj.Text,
                                Session = responseObject.Session,
                                Id = newExtObj.Id,
                                FullId = workspace.Id + responseObject.Session + newExtObj.Id
                            };
                            await externalObjects_repository.AddAsync(extObjectForAdding);

                            workspace.ExternalObjects.Add(extObjectForAdding);
                            extObjectForAdding.Workspaces.Add(workspace);

                            workspace.EntitiesCount++;
                        }
                    }

                    workspace.IsAllDataEmbedded = false;

                    await repository.SaveChangesAsync();

                    await taskService.ChangeTaskPercent(taskId, 100f);
                    await taskService.ChangeTaskState(taskId, TaskStates.Completed);
                }
                else
                {
                    await taskService.ChangeTaskState(taskId, TaskStates.Error);
                }
            }
            catch (Exception ex)
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
        }
        #endregion
    }
}
