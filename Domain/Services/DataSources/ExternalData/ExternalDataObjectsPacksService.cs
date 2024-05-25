using Accord.Math;
using AutoMapper;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using Domain.DTOs.ExternalData;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Youtube;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DataSources.ExternalData;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Domain.Resources.Types.Clusterization;
using Domain.Resources.Types.DataSources;
using Domain.Resources.Types.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TL;
using TL.Methods;

namespace Domain.Services.DataSources.ExternalData
{
    public class ExternalDataObjectsPacksService:IExternalDataObjectsPacksService
    {
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _addPacksRepository;
        private readonly IRepository<ExternalObject> _externalObjectsRepository;
        private readonly IRepository<ExternalObjectsPack> _externalObjectsPacksRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;
        private readonly IQuotasControllerService _quotasControllerService;
        private readonly IMapper _mapper;

        public ExternalDataObjectsPacksService(IRepository<ExternalObjectsPack> externalObjectsPacksRepository,
            IRepository<MyDataObject> dataObjectsRepository,
            IRepository<WorkspaceDataObjectsAddPack> addPacksRepository,
            IRepository<ExternalObject> externalObjectsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IStringLocalizer<TaskTitles> tasksLocalizer,
            IUserService userService,
            IBackgroundJobClient backgroundJobClient,
            IMyTasksService tasksService,
            IEmbeddingLoadingStatesService embeddingLoadingStatesService,
            IQuotasControllerService quotasControllerService,
            IRepository<ClusterizationWorkspace> workspacesRepository,
            IMapper mapper)
        {
            _dataObjectsRepository = dataObjectsRepository;
            _addPacksRepository = addPacksRepository;
            _externalObjectsRepository = externalObjectsRepository;
            _localizer = localizer;
            _tasksLocalizer = tasksLocalizer;
            _userService = userService;
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
            _externalObjectsPacksRepository = externalObjectsPacksRepository;
            _quotasControllerService = quotasControllerService;
            _workspacesRepository = workspacesRepository;
            _mapper = mapper;
        }
        public async Task LoadExternalDataObject(AddExternalDataRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.LoadingExternalDataObjects]
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            var objectsList = await LoadObjectsListFromFile(request.File, userId, taskId);

            var newRequest = new AddExternalDataWithoutFileRequest()
            {
                ChangingType = request.ChangingType,
                Description = request.Description,
                Title = request.Title,
                ObjectsList = objectsList,
                VisibleType = request.VisibleType,
                WorkspaceId = request.WorkspaceId
            };

            _backgroundJobClient.Enqueue(() => LoadExternalDataObjectBackgroundJob(newRequest, userId, taskId));
        }

        public async Task<List<ExternalObjectModelDTO>> LoadObjectsListFromFile(IFormFile file, string userId, string taskId)
        {
            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;
            try
            {
                if (file != null && file.Length > 0)
                {
                    var fileContent = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    var objectsList = new List<ExternalObjectModelDTO>();

                    try
                    {
                        var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalDataFileModelDTO>(fileContent);
                        objectsList = res.ExternalObjects.Where(e => e.Text != "").ToList();
                    }
                    catch
                    {
                        try
                        {
                            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExternalObjectModelDTO>>(fileContent);
                            objectsList = res.Where(e => e.Text != "").ToList();
                        }
                        catch
                        {
                            var list = fileContent.Split("\n");

                            foreach (var str in list)
                            {
                                if (str != null && str != "")
                                {
                                    objectsList.Add(new ExternalObjectModelDTO()
                                    {
                                        Id = "",
                                        Text = str
                                    });
                                }
                            }
                        }
                    }

                    if (objectsList == null || objectsList.Count == 0) throw new HttpException(_localizer[ErrorMessagePatterns.FileError], HttpStatusCode.BadRequest);

                    return objectsList;
                }
                else
                {
                    await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                    await _tasksService.ChangeTaskDescription(taskId, "File is unavailable");

                    return null;
                }
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);

                return null;
            }
        }
        public async Task<int?> LoadExternalDataObjectBackgroundJob(AddExternalDataWithoutFileRequest request, string userId, string taskId)
        {
            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                if (request.ObjectsList == null || request.ObjectsList.Count == 0) throw new HttpException(_localizer[ErrorMessagePatterns.FileError], HttpStatusCode.BadRequest);

                var pack = new ExternalObjectsPack()
                {
                    ChangingType = request.ChangingType,
                    VisibleType = request.VisibleType,
                    Title = request.Title,
                    Description = request.Description,
                    OwnerId = userId,
                };
                await _externalObjectsPacksRepository.AddAsync(pack);

                var logs = Guid.NewGuid().ToString();
                foreach (var newExtObj in request.ObjectsList)
                {
                    var extObjectForAdding = new ExternalObject()
                    {
                        Text = newExtObj.Text,
                        ExternalId = newExtObj.Id,
                        Pack = pack
                    };
                    await _externalObjectsRepository.AddAsync(extObjectForAdding);

                    pack.ExternalObjectsCount++;

                    var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.ExternalData, 1, logs);

                    if (!quotasResult)
                    {
                        await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                        await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                        return null;
                    }
                    pack.ExternalObjectsCount++;
                }

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);

                return pack.Id;
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);

                return null;
            }
        }

        public async Task AddExternalDataObjectsToWorkspace(AddExternalDataObjectsPacksToWorkspaceRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                WorkspaceId = request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingExternalDataToWorkspace]
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => AddExternalDataObjectsToWorkspaceBackgroundJob(request, userId, taskId));
        }
        public async Task AddExternalDataObjectsToWorkspaceBackgroundJob(AddExternalDataObjectsPacksToWorkspaceRequest request, string userId, string taskId)
        {

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();
                if (workspace == null || workspace.TypeId != ClusterizationTypes.External || (workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId))) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], System.Net.HttpStatusCode.BadRequest);

                if (workspace.IsProfilesInCalculation) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceProfilesInCalculation], HttpStatusCode.BadRequest);

                Expression<Func<ExternalObject, bool>> filterCondition = e => e.PackId == request.PackId;

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
                    var externalDataObjects = await _externalObjectsRepository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(ExternalObject.DataObject)}", pageParameters: new DTOs.PageParameters()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });


                    int count = 0;
                    foreach (var externalObject in externalDataObjects)
                    {
                        MyDataObject dataObject;
                        if (externalObject.DataObject == null)
                        {
                            dataObject = new MyDataObject()
                            {
                                ExternalObject = externalObject,
                                TypeId = DataObjectTypes.ExternalData,
                                Text = externalObject.Text,
                            };

                            await _dataObjectsRepository.AddAsync(dataObject);
                            await _dataObjectsRepository.SaveChangesAsync();
                        }
                        else
                        {
                            dataObject = externalObject.DataObject;
                            pack.DataObjects.Add(dataObject);
                        }

                        if (!workspace.DataObjects.Contains(dataObject))
                        {
                            pack.DataObjects.Add(dataObject);
                            dataObject.WorkspaceDataObjectsAddPacks.Add(pack);

                            workspace.DataObjects.Add(dataObject);
                            count++;
                            addedElementsCount++;
                            workspace.EntitiesCount++;
                        }
                    }
                    await _workspacesRepository.SaveChangesAsync();

                    if (externalDataObjects.Count() < pageSize) break;
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
                await _tasksService.ChangeParentTaskState(taskId, TaskStates.Error);
            }
        }

        public async Task LoadExternalDataAndAddToWorkspace(AddExternalDataRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.LoadingAllEmbeddingsInWorkspace],
                IsGroupTask = true
            };
            var groupTaskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            var objectsList = await LoadObjectsListFromFile(request.File, userId, groupTaskId);

            var newRequest = new AddExternalDataWithoutFileRequest()
            {
                ChangingType = request.ChangingType,
                Description = request.Description,
                Title = request.Title,
                ObjectsList = objectsList,
                VisibleType = request.VisibleType,
                WorkspaceId = request.WorkspaceId
            };

            var createSubTask1 = new CreateSubTaskOptions()
            {
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.LoadingExternalDataObjects],
                GroupTaskId = groupTaskId,
                Position = 1
            };
            var taskId1 = await _tasksService.CreateSubTaskWithUserId(createSubTask1);

            var createSubTask2 = new CreateSubTaskOptions()
            {
                WorkspaceId=request.WorkspaceId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingExternalDataToWorkspace],
                GroupTaskId = groupTaskId,
                Position = 2
            };
            var taskId2 = await _tasksService.CreateSubTaskWithUserId(createSubTask2);

            _backgroundJobClient.Enqueue(() => LoadExternalDataAndAddToWorkspaceBackgroundJob(newRequest, userId, groupTaskId, new List<string> { taskId1, taskId2 }));
        }
        public async Task LoadExternalDataAndAddToWorkspaceBackgroundJob(AddExternalDataWithoutFileRequest request, string userId, string groupTaskId, List<string> subTaskIds)
        {
            var packId = await LoadExternalDataObjectBackgroundJob(request, userId, subTaskIds[0]);

            if (packId == null)
            {
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.ErrorWhileLoadExternalData]);
                return;
            }
            await AddExternalDataObjectsToWorkspaceBackgroundJob(new AddExternalDataObjectsPacksToWorkspaceRequest()
            {
                PackId = (int)packId,
                WorkspaceId = (int)request.WorkspaceId
            }, userId, subTaskIds[1]);
        }
        
        
        
        public async Task UpdatePack(UpdateExternalDataPackRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var pack = await _externalObjectsPacksRepository.FindAsync(request.Id);
            if (pack == null || pack.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.ExternalObjectsNotFound], HttpStatusCode.NotFound);

            pack.ChangingType = request.ChangingType;
            pack.VisibleType = request.VisibleType;
            pack.Title = request.Title;
            pack.Description = request.Description;
            
            pack.LastEditTime = DateTime.UtcNow;
            pack.IsEdited = true;

            await _externalObjectsPacksRepository.SaveChangesAsync();
        }

        public async Task<ICollection<SimpleExternalObjectsPackDTO>> GetCollection(GetExternalDataObjectsPacksRequest request)
        {
            Expression<Func<ExternalObjectsPack, bool>> filterCondition = e => true;

            if (request.FilterStr != null && request.FilterStr != "")
            {
                filterCondition = e => e.Title.Contains(request.FilterStr);
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

            var packs = await _externalObjectsPacksRepository.GetAsync(filter: filterCondition, pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<SimpleExternalObjectsPackDTO>>(packs);
        }
        public async Task<ICollection<SimpleExternalObjectsPackDTO>> GetCustomerCollection(GetExternalDataObjectsPacksRequest request)
        {
            var customerId = await _userService.GetCurrentUserId();
            if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            Expression<Func<ExternalObjectsPack, bool>> filterCondition = e => e.OwnerId == customerId;

            if (!string.IsNullOrEmpty(request.FilterStr))
            {
                filterCondition = filterCondition.And(e => e.Title.Contains(request.FilterStr));
            }

            if (customerId != null)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers || e.OwnerId == customerId);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }

            var packs = await _externalObjectsPacksRepository.GetAsync(filter: filterCondition, pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<SimpleExternalObjectsPackDTO>>(packs);
        }
        public async Task<FullExternalObjectsPackDTO> GetFullById(int id)
        {
            var pack = await _externalObjectsPacksRepository.FindAsync(id);
            if (pack == null) throw new HttpException(_localizer[ErrorMessagePatterns.ExternalObjectsNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<FullExternalObjectsPackDTO>(pack);
        }
    }
}
