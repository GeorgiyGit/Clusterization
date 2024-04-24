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

namespace Domain.Services.Embeddings.EmbeddingsLoading
{
    public class EmbeddingsLoadingService : IEmbeddingsLoadingService
    {

        private readonly IRepository<WorkspaceDataObjectsAddPack> _dataPacksRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository;
        private readonly IRepository<ClusterizationProfile> _profilesRepository;

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
                                     IEmbeddingLoadingStatesService embeddingLoadingStatesService)
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
        }
        public async Task LoadEmbeddingsByProfile(int profileId)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingAllEmbeddingsInWorkspace]);

            _backgroundJobClient.Enqueue(() => LoadEmbeddingsByProfileBackgroundJob(profileId, userId, taskId));
        }
        public async Task LoadEmbeddingsByProfileBackgroundJob(int profileId, string userId, int taskId)
        {
            var profile = (await _profilesRepository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.EmbeddingLoadingState)}")).FirstOrDefault();

            if (profile == null || (profile.ChangingType == ChangingTypes.OnlyOwner && profile.OwnerId != userId))
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.ProfileChangingTypeIsOnlyOwner]);
            }
            var workspace = (await _workspacesRepository.GetAsync(c => c.Id == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.WorkspaceDataObjectsAddPacks)}")).FirstOrDefault();

            if (workspace == null || (workspace.ChangingType == ChangingTypes.OnlyOwner && workspace.OwnerId != userId))
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner]);
            }
            if (profile.EmbeddingLoadingState.IsAllEmbeddingsLoaded)
            {
                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
                return;
            }

            bool flag = false;
            var packs = workspace.WorkspaceDataObjectsAddPacks.Where(e => !e.IsDeleted);
            foreach (var pack in packs)
            {
                var packTaskId = await _tasksService.CreateTaskWithUserId(_tasksLocalizer[TaskTitlesPatterns.LoadingEmbeddingsPack], userId);

                await LoadEmbeddingsByWorkspaceDataPackBackgroundJob(pack.Id, userId, packTaskId, profile.EmbeddingModelId);

                var taskState = await _tasksService.GetTaskStateId(packTaskId);

                if (taskState == TaskStates.Error) flag = true;
            }

            if (flag)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, "Not all packs were loaded");
            }
            else
            {
                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
            }

        }
        public async Task LoadEmbeddingsByWorkspaceDataPack(int packId, string embeddingModelId)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingEmbeddingsPack]);

            _backgroundJobClient.Enqueue(() => LoadEmbeddingsByWorkspaceDataPackBackgroundJob(packId, userId, taskId, embeddingModelId));
        }

        public async Task LoadEmbeddingsByWorkspaceDataPackBackgroundJob(int packId, string userId, int taskId, string embeddingModelId)
        {
            try
            {
                var pack = (await _dataPacksRepository.GetAsync(e => e.Id == packId, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.Workspace)},{nameof(WorkspaceDataObjectsAddPack.DataObjects)},{nameof(WorkspaceDataObjectsAddPack.EmbeddingLoadingStates)}")).FirstOrDefault();

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


                var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Embeddings, pack.DataObjects.Count() * embeddingModel.QuotasPrice, Guid.NewGuid().ToString());

                if (!quotasResult)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                }

                var dataObjects = pack.DataObjects.ToList();
                var dataObjectStrings = pack.DataObjects.Select(e => e.Text).ToList();

                var result = await LoadEmbeddings(embeddingModelId, dataObjectStrings);

                if (!result.Successful)
                {
                    await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                    await _tasksService.ChangeTaskDescription(taskId, result.Error.Message);
                    return;
                }

                await _tasksService.ChangeTaskPercent(taskId, 50f);

                var maxDimensionCount = embeddingModel.DimensionTypeId;

                var percents = 0f;
                for (int i = 0; i < result.Data.Count(); i++)
                {
                    await _embeddingsService.AddEmbeddingsToDataObject(result.Data[i].Embedding, pack.WorkspaceId, DimensionalityReductionTechniques.JSL, embeddingModelId, dataObjects[i].Id, maxDimensionCount);

                    percents += 1f / (float)result.Data.Count() * 100f;
                    await _tasksService.ChangeTaskPercent(taskId, percents);
                }

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);

                packState.IsAllEmbeddingsLoaded = true;
                await _embeddingModelsRepository.SaveChangesAsync();

                await _embeddingLoadingStatesService.ReviewStates(workspace.Id);
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
