using Domain.Entities.Clusterization.Workspaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Embeddings.EmbeddingsLoading;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Hangfire;
using Microsoft.Extensions.Localization;
using Domain.Exceptions;
using Domain.Resources.Types;
using System.Net;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using Domain.Entities.DataObjects;
using Accord.Math;
using Domain.Entities.EmbeddingModels;
using Domain.Interfaces.Other;
using OpenAI.ObjectModels.ResponseModels;
using Domain.Entities.Clusterization.Profiles;
using Domain.Interfaces.Embeddings;
using Domain.Resources.Types.Clusterization;
using Domain.DTOs;
using Domain.Entities.Embeddings;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Resources.Types.Tasks;
using AutoMapper;
using TL;

namespace Domain.Services.Embeddings.EmbeddingsLoading
{
    public class EmbeddingsLoadingService : IEmbeddingsLoadingService
    {

        private readonly IRepository<WorkspaceDataObjectsAddPack> _dataPacksRepository;
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository; 
        private readonly IRepository<ClusterizationProfile> _profilesRepository;
        private readonly IRepository<EmbeddingObjectsGroup> _embeddingObjectsGroupsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        private readonly IOpenAIService _openAIService;
        private readonly IEmbeddingsService _embeddingsService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;

        public EmbeddingsLoadingService(IStringLocalizer<ErrorMessages> localizer,
                                     IBackgroundJobClient backgroundJobClient,
                                     IMyTasksService tasksService,
                                     IUserService userService,
                                     IStringLocalizer<TaskTitles> tasksLocalizer,
                                     IQuotasControllerService quotasControllerService,
                                     IRepository<WorkspaceDataObjectsAddPack> dataPacksRepository,
                                     IRepository<ClusterizationWorkspace> workspacesRepository,
                                     IOpenAIService openAIService,
                                     IEmbeddingsService embeddingsService,
                                     IRepository<EmbeddingModel> embeddingModelsRepository,
                                     IRepository<ClusterizationProfile> profilesRepository,
                                     IEmbeddingLoadingStatesService embeddingLoadingStatesService,
                                     IRepository<MyDataObject> dataObjectsRepository,
                                     IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository)
        {
            _localizer = localizer;
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _userService = userService;
            _tasksLocalizer = tasksLocalizer;
            _quotasControllerService = quotasControllerService;
            _dataPacksRepository = dataPacksRepository;
            _workspacesRepository = workspacesRepository;
            _openAIService = openAIService;
            _embeddingsService = embeddingsService;
            _embeddingModelsRepository = embeddingModelsRepository;
            _profilesRepository = profilesRepository;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
            _dataObjectsRepository = dataObjectsRepository;
            _embeddingObjectsGroupsRepository = embeddingObjectsGroupsRepository;
        }
        public async Task LoadEmbeddingsByProfile(int profileId)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var profile = await _profilesRepository.FindAsync(profileId);
            var createTaskOptions = new CreateMainTaskOptions()
            {
                ClusterizationProfileId=profileId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.LoadingAllEmbeddingsInWorkspace],
                IsGroupTask = true
            };
            if (profile != null) createTaskOptions.WorkspaceId = profile.WorkspaceId;

            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            var subTaskIds = await CreateSubTasks(profileId, userId, taskId, 1);

            _backgroundJobClient.Enqueue(() => LoadEmbeddingsByProfileBackgroundJob(profileId, userId, taskId, subTaskIds));
        }
        public async Task LoadEmbeddingsByProfileBackgroundJob(int profileId, string userId, string groupTaskId, List<string> subTaskIds)
        {
            await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Process);

            var profile = (await _profilesRepository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.EmbeddingLoadingState)}")).FirstOrDefault();

            if (profile == null || (profile.ChangingType == ChangingTypes.OnlyOwner && profile.OwnerId != userId))
            {
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.ProfileChangingTypeIsOnlyOwner]);
            }
            var workspace = (await _workspacesRepository.GetAsync(c => c.Id == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.WorkspaceDataObjectsAddPacks)}")).FirstOrDefault();

            if (workspace == null || (workspace.ChangingType == ChangingTypes.OnlyOwner && workspace.OwnerId != userId))
            {
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner]);
            }
            if (profile.EmbeddingLoadingState.IsAllEmbeddingsLoaded)
            {
                await _tasksService.ChangeTaskPercent(groupTaskId, 100f);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Completed);
                return;
            }

            bool flag = false;
            var packs = workspace.WorkspaceDataObjectsAddPacks.Where(e => !e.IsDeleted).ToList();

            for (int i = 0; i < packs.Count(); i++)
            {
                var pack = packs[i];

                await LoadEmbeddingsByWorkspaceDataPackBackgroundJob(pack.Id, userId, subTaskIds[i], profile.EmbeddingModelId);

                var taskState = await _tasksService.GetTaskStateId(subTaskIds[i]);

                if (taskState == TaskStates.Error) flag = true;
            }

            if (flag)
            {
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.NotAllPacksWereLoaded]);
            }
            else
            {
                await _tasksService.ChangeTaskPercent(groupTaskId, 100f);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Completed);
            }
        }
        public async Task LoadEmbeddingsByWorkspaceDataPack(int packId, string embeddingModelId)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var pack = await _dataPacksRepository.FindAsync(packId);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                AddPackId = packId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.LoadingEmbeddingsPack],
            };
            if (pack != null) createTaskOptions.WorkspaceId = pack.WorkspaceId;

            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => LoadEmbeddingsByWorkspaceDataPackBackgroundJob(packId, userId, taskId, embeddingModelId));
        }

        public async Task<List<string>> CreateSubTasks(int profileId, string userId, string groupTaskId, int subTasksPos)
        {
            var profile = (await _profilesRepository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.EmbeddingLoadingState)}")).FirstOrDefault();
            if (profile == null) return null;

            var workspace = (await _workspacesRepository.GetAsync(c => c.Id == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.WorkspaceDataObjectsAddPacks)}")).FirstOrDefault();
            if (workspace == null) return null;

            var packs = workspace.WorkspaceDataObjectsAddPacks.Where(e => !e.IsDeleted).ToList();

            List<string> taskIds = new List<string>(packs.Count());
            for (int i = 0; i < packs.Count(); i++)
            {
                var createTaskOptions = new CreateSubTaskOptions()
                {
                    AddPackId = packs[i].Id,
                    WorkspaceId = workspace.Id,
                    CustomerId = userId,
                    Title = _tasksLocalizer[TaskTitlesPatterns.LoadingEmbeddingsPack],
                    Position = subTasksPos + i,
                    GroupTaskId = groupTaskId
                };
                var packTaskId = await _tasksService.CreateSubTaskWithUserId(createTaskOptions);
                taskIds.Add(packTaskId);
            }
            return taskIds;
        }

        public async Task LoadEmbeddingsByWorkspaceDataPackBackgroundJob(int packId, string userId, string taskId, string embeddingModelId)
        {
            try
            {
                var pack = (await _dataPacksRepository.GetAsync(e => e.Id == packId, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.Workspace)},{nameof(WorkspaceDataObjectsAddPack.EmbeddingLoadingStates)}")).FirstOrDefault();

                if (pack == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceDataObjectsAddPackNotFound], System.Net.HttpStatusCode.NotFound);

                var workspace = (await _workspacesRepository.GetAsync(c => c.Id == pack.WorkspaceId)).FirstOrDefault();
                

                if (workspace == null || (workspace.ChangingType == ChangingTypes.OnlyOwner && workspace.OwnerId != userId)) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

                var embeddingModel = (await _embeddingModelsRepository.GetAsync(e => e.Id == embeddingModelId)).FirstOrDefault();

                var packState = pack.EmbeddingLoadingStates.Where(e => e.EmbeddingModelId == embeddingModelId).FirstOrDefault();
                if (packState==null || packState.IsAllEmbeddingsLoaded)
                {
                    await _tasksService.ChangeTaskPercent(taskId, 100f);
                    await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
                    return;
                }

                var stateId = await _tasksService.GetTaskStateId(taskId);
                if (stateId != TaskStates.Wait) return;

                await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

                var logsId = Guid.NewGuid().ToString();

                const int pageSize = 500;
                var pageParameters = new PageParameters()
                {
                    PageNumber = 1,
                    PageSize = pageSize
                };
                if (pack.DataObjectsCount < pageSize) pageParameters.PageSize = pack.DataObjectsCount;

                int loadedCount = 0;
                var percents = 0f;
                while (true)
                {
                    var dataObjects = (await _dataObjectsRepository.GetAsync(e => e.WorkspaceDataObjectsAddPacks.Contains(pack),
                                                                             pageParameters: pageParameters)).ToList();
                    
                    if (dataObjects.Count() == 0) break;
                    List<MyDataObject> filteredDataObjects = new List<MyDataObject>();

                    foreach(var dataObject in dataObjects)
                    {
                        var embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DRTechniqueId == DimensionalityReductionTechniques.JSL && e.EmbeddingModelId == embeddingModelId && e.DataObjectId == dataObject.Id, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();

                        if (embeddingObjectsGroup == null)
                        {
                            filteredDataObjects.Add(dataObject);
                        }
                    }

                    var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Embeddings, filteredDataObjects.Count() * embeddingModel.QuotasPrice, logsId);
                    if (!quotasResult)
                    {
                        throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                    }

                    var dataObjectStrings = filteredDataObjects.Select(e => e.Text.Length > embeddingModel.MaxInputCount ? e.Text.Substring(0, embeddingModel.MaxInputCount) : e.Text).ToList();

                    EmbeddingCreateResponse result = new EmbeddingCreateResponse();

                    if (filteredDataObjects.Count > 0)
                    {
                        for (int i = 0; i < dataObjectStrings.Count; i++)
                        {
                            if (dataObjectStrings[i] == null || dataObjectStrings[i] == "")
                            {
                                dataObjectStrings[i] = "-";
                            }
                        }

                        result = await LoadEmbeddings(embeddingModelId, dataObjectStrings);

                        if (!result.Successful)
                        {
                            await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                            await _tasksService.ChangeTaskDescription(taskId, result.Error.Message);
                            return;
                        }
                    }

                    await _tasksService.ChangeTaskPercent(taskId, 50f);

                    var maxDimensionCount = embeddingModel.DimensionTypeId;

                    var resId = 0;
                    for (int i = 0; i < dataObjects.Count(); i++)
                    {
                        var dataObject = dataObjects[i];

                        if (filteredDataObjects.Contains(dataObject))
                        {
                            await _embeddingsService.AddEmbeddingsToDataObject(result.Data[resId].Embedding, pack.WorkspaceId, DimensionalityReductionTechniques.JSL, embeddingModelId, dataObject.Id, maxDimensionCount);
                            resId++;
                        }
                        else
                        {
                            var embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DRTechniqueId == DimensionalityReductionTechniques.JSL && e.EmbeddingModelId == embeddingModelId && e.DataObjectId == dataObject.Id, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();

                            var origEmbeddingGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DRTechniqueId == DimensionalityReductionTechniques.JSL && e.WorkspaceId==workspace.Id && e.EmbeddingModelId == embeddingModelId && e.DataObjectId == dataObject.Id, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();

                            if (origEmbeddingGroup == null)
                            {
                                var embeddings = embeddingObjectsGroup.DimensionEmbeddingObjects.Where(e => e.TypeId == embeddingModel.DimensionTypeId).FirstOrDefault().ValuesString.Split(' ').Select(double.Parse).ToList();
                                await _embeddingsService.AddEmbeddingsToDataObject(embeddings, pack.WorkspaceId, DimensionalityReductionTechniques.JSL, embeddingModelId, dataObject.Id, maxDimensionCount);
                            }
                        }

                        percents += 1f / (float)pack.DataObjectsCount * 100f;
                        await _tasksService.ChangeTaskPercent(taskId, percents);
                        loadedCount++;
                    }

                    await _tasksService.ChangeTaskPercent(taskId, percents);

                    await _embeddingModelsRepository.SaveChangesAsync();

                    if (loadedCount >= pack.DataObjectsCount) break;

                    pageParameters.PageNumber++;
                }
                packState.IsAllEmbeddingsLoaded = true;

                await _embeddingLoadingStatesService.ReviewStates(workspace.Id);

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }    
        private async Task<EmbeddingCreateResponse> LoadEmbeddings(string embeddingModelId, List<string> dataObjects)
        {
            if (embeddingModelId == EmbeddingModelList.text_embedding_ada_002)
            {
                return await _openAIService.Embeddings.CreateEmbedding(new EmbeddingCreateRequest()
                {
                    EncodingFormat = "float",
                    Model = Models.TextEmbeddingAdaV2,
                    InputAsList = dataObjects
                });
            }
            else if (embeddingModelId == EmbeddingModelList.text_embedding_3_large)
            {
                return await _openAIService.Embeddings.CreateEmbedding(new EmbeddingCreateRequest()
                {
                    EncodingFormat = "float",
                    Model = Models.TextEmbeddingV3Large,
                    InputAsList = dataObjects
                });
            }
            else if (embeddingModelId == EmbeddingModelList.text_embedding_3_small)
            {
                return await _openAIService.Embeddings.CreateEmbedding(new EmbeddingCreateRequest()
                {
                    EncodingFormat = "float",
                    Model = Models.TextEmbeddingV3Small,
                    InputAsList = dataObjects
                });
            }
            else
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.EmbeddingsLoadingError], HttpStatusCode.BadRequest);
            }
        }
    }
}
