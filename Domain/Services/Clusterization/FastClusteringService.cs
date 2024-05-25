using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.DTOs.ClusterizationDTOs.FastClusteringDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.ExternalData;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.DTOs.QuotaDTOs.CustomerQuotasDTOs.Responses;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.FastClustering;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Quotas;
using Domain.Exceptions;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Interfaces.Clusterization.Workspaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DataSources.ExternalData;
using Domain.Interfaces.Embeddings.EmbeddingsLoading;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Domain.Resources.Types.Clusterization;
using Domain.Resources.Types.Tasks;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;

namespace Domain.Services.Clusterization
{
    public class FastClusteringService: IFastClusteringService
    {
        private readonly IRepository<FastClusteringWorkflow> _fastClusteringWorkflowsRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _workspaceAddDataPacksRepository;
        private readonly IRepository<ClusterizationAbstactAlgorithm> _abstractAlgorithmsRepository;
        private readonly IRepository<QuotasType> _quotasTypesRepository;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IClusterizationWorkspacesService _workspacesService;
        private readonly IClusterizationProfilesService _profilesService;
        private readonly IExternalDataObjectsPacksService _externalDataObjectsPacksService;
        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IEmbeddingsLoadingService _embeddingsLoadingService;
        private readonly IMapper _mapper;

        private readonly IGeneralClusterizationAlgorithmService _generalClusterizationAlgorithmService;
        private readonly IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmRequest, KMeansAlgorithmDTO> _kMeansService;
        private readonly IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmRequest, OneClusterAlgorithmDTO> _oneClusterService;
        private readonly IAbstractClusterizationAlgorithmService<AddDBSCANAlgorithmRequest, DBSCANAlgorithmDTO> _dbSCANService;
        private readonly IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO> _spectralClusteringService;
        private readonly IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmRequest, GaussianMixtureAlgorithmDTO> _gaussianMixtureService;

        public FastClusteringService(IRepository<FastClusteringWorkflow> fastClusteringWorkflowsRepository,
            IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<WorkspaceDataObjectsAddPack> workspaceAddDataPacksRepository,
            IRepository<ClusterizationAbstactAlgorithm> abstractAlgorithmsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IStringLocalizer<TaskTitles> tasksLocalizer,
            IClusterizationWorkspacesService workspacesService,
            IClusterizationProfilesService profilesService,
            IExternalDataObjectsPacksService externalDataObjectsPacksService,
            IUserService userService,
            IBackgroundJobClient backgroundJobClient,
            IMyTasksService tasksService,
            IEmbeddingsLoadingService embeddingsLoadingService,
            IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmRequest,KMeansAlgorithmDTO> kMeansService,
            IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmRequest, OneClusterAlgorithmDTO> oneClusterService,
            IAbstractClusterizationAlgorithmService<AddDBSCANAlgorithmRequest, DBSCANAlgorithmDTO> dbSCANService,
            IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO> spectralClusteringService,
            IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmRequest, GaussianMixtureAlgorithmDTO> gaussianMixtureService,
            IMapper mapper,
            IRepository<QuotasType> quotasTypesRepository,
            IRepository<EmbeddingModel> embeddingModelsRepository,
            IGeneralClusterizationAlgorithmService generalClusterizationAlgorithmService)
        {
            _fastClusteringWorkflowsRepository = fastClusteringWorkflowsRepository;
            _workspacesRepository = workspacesRepository;
            _workspaceAddDataPacksRepository = workspaceAddDataPacksRepository;
            _abstractAlgorithmsRepository = abstractAlgorithmsRepository;
            _localizer = localizer;
            _tasksLocalizer = tasksLocalizer;
            _workspacesService = workspacesService;
            _profilesService = profilesService;
            _externalDataObjectsPacksService = externalDataObjectsPacksService;
            _userService = userService;
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _embeddingsLoadingService = embeddingsLoadingService;
            _kMeansService = kMeansService;
            _oneClusterService = oneClusterService;
            _dbSCANService = dbSCANService;
            _spectralClusteringService = spectralClusteringService;
            _gaussianMixtureService = gaussianMixtureService;
            _mapper = mapper;
            _quotasTypesRepository = quotasTypesRepository;
            _embeddingModelsRepository = embeddingModelsRepository;
            _generalClusterizationAlgorithmService = generalClusterizationAlgorithmService;
        }

        public async Task<int> CreateWorkflow()
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var workflow = (await _fastClusteringWorkflowsRepository.GetAsync(e=>e.OwnerId== userId)).FirstOrDefault();
            if (workflow != null) return workflow.Id;

            var newWorkflow = new FastClusteringWorkflow()
            {
                OwnerId = userId
            };
            
            await _fastClusteringWorkflowsRepository.AddAsync(newWorkflow);
            await _fastClusteringWorkflowsRepository.SaveChangesAsync();

            return newWorkflow.Id;
        }

        public async Task<int> InitializeWorkspace(FastClusteringInitialRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var workflow = (await _fastClusteringWorkflowsRepository.GetAsync(e => e.OwnerId == userId)).FirstOrDefault();
            if (workflow == null) throw new HttpException(_localizer[ErrorMessagePatterns.FastClusteringWorkflowNotFound], HttpStatusCode.NotFound);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.InitializingWorkspace],
                SubTasksCount = 3,
                IsGroupTask = true
            };
            var groupTaskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            var createSubTask1 = new CreateSubTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingWorkspace],
                GroupTaskId = groupTaskId,
                Position = 1
            };
            var taskId1 = await _tasksService.CreateSubTaskWithUserId(createSubTask1);


            await _tasksService.ChangeTaskState(taskId1, TaskStates.Process);

            int workspaceId;
            string newTitle;
            string newDescription;
            try
            {
                var addWorkspaceRequest = new AddClusterizationWorkspaceRequest()
                {
                    ChangingType = ChangingTypes.OnlyOwner,
                    VisibleType = ChangingTypes.OnlyOwner,
                    TypeId = ClusterizationTypes.External
                };


                if (request.Title == null || request.Title == "") newTitle = "Fast clustering";
                else newTitle = request.Title;


                if (request.Description == null || request.Description == "") newDescription = "Fast clustering";
                else newDescription = request.Description;

                addWorkspaceRequest.Title = newTitle;
                addWorkspaceRequest.Description = newDescription;

                workspaceId = await _workspacesService.Add(addWorkspaceRequest);

                var origWorkspace = (await _workspacesRepository.GetAsync(e => e.Id == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.FastClusteringWorkflow)}")).FirstOrDefault();
                if (origWorkspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

                origWorkspace.FastClusteringWorkflowId = workflow.Id;
                await _fastClusteringWorkflowsRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskDescription(taskId1, ex.Message);
                await _tasksService.ChangeTaskState(taskId1, TaskStates.Error);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                throw ex;
            }
            await _tasksService.ChangeTaskPercent(taskId1, 100f);
            await _tasksService.ChangeTaskState(taskId1, TaskStates.Completed);

            _backgroundJobClient.Enqueue(() => InitializeWorkspaceBackgroundJob(request, userId, groupTaskId, workspaceId, 2, newTitle, newDescription));

            return workspaceId;
        }
        public async Task InitializeWorkspaceBackgroundJob(FastClusteringInitialRequest request, string userId, string groupTaskId, int workspaceId, int subTasksPos, string newTitle, string newDescription)
        {
            var stateId = await _tasksService.GetTaskStateId(groupTaskId);
            if (stateId != TaskStates.Wait) return;

            var objectsList = new List<ExternalObjectModelDTO>(request.Texts.Count);

            for (int i = 0; i < request.Texts.Count; i++) {
                objectsList.Add(new ExternalObjectModelDTO()
                {
                    Id = i + "",
                    Text = request.Texts.ElementAt(i)
                });
            }

            var addDataRequest = new AddExternalDataWithoutFileRequest()
            {
                ChangingType = ChangingTypes.OnlyOwner,
                VisibleType = ChangingTypes.OnlyOwner,
                Description = newDescription,
                Title = newTitle,
                WorkspaceId = workspaceId,
                ObjectsList = objectsList
            };

            await _externalDataObjectsPacksService.LoadExternalDataAndAddToWorkspaceBackgroundJob(addDataRequest, userId, groupTaskId, subTasksPos);
        }

        public async Task<int> InitializeProfile(FastClusteringProcessRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var workflow = (await _fastClusteringWorkflowsRepository.GetAsync(e => e.OwnerId == userId)).FirstOrDefault();
            if (workflow == null) throw new HttpException(_localizer[ErrorMessagePatterns.FastClusteringWorkflowNotFound], HttpStatusCode.NotFound);

            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.FastClusteringWorkflow)}")).FirstOrDefault();
            if (workspace == null || workspace.FastClusteringWorkflow == null || workspace.FastClusteringWorkflowId != workflow.Id) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.Clustering],
                SubTasksCount = 4,
                IsGroupTask = true
            };
            var groupTaskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            var createSubTask1 = new CreateSubTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingWorkspace],
                GroupTaskId = groupTaskId,
                Position = 1
            };
            var taskId1 = await _tasksService.CreateSubTaskWithUserId(createSubTask1);


            await _tasksService.ChangeTaskState(taskId1, TaskStates.Process);

            int profileId;
            string newTitle;
            string newDescription;
            try
            {
                var addProfileRequest = new AddClusterizationProfileRequest()
                {
                    ChangingType = ChangingTypes.OnlyOwner,
                    VisibleType = ChangingTypes.OnlyOwner,
                    WorkspaceId = request.WorkspaceId,
                    AlgorithmId = request.AlgorithmId,
                    EmbeddingModelId = request.EmbeddingModelId,
                    DimensionCount = request.DimensionCount,
                    DRTechniqueId = request.DRTechniqueId,
                };

                profileId = await _profilesService.Add(addProfileRequest);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskDescription(taskId1, ex.Message);
                await _tasksService.ChangeTaskState(taskId1, TaskStates.Error);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                throw ex;
            }
            await _tasksService.ChangeTaskPercent(taskId1, 100f);
            await _tasksService.ChangeTaskState(taskId1, TaskStates.Completed);

            _backgroundJobClient.Enqueue(() => InitializeProfileBackgroundJob(profileId, userId, groupTaskId, 2, request.AlgorithmId, request.WorkspaceId));

            return profileId;
        }
        public async Task InitializeProfileBackgroundJob(int profileId, string userId, string groupTaskId, int subTasksPos, int algorithmId, int workspaceId)
        {
            var stateId = await _tasksService.GetTaskStateId(groupTaskId);
            if (stateId != TaskStates.Wait) return;

            await _embeddingsLoadingService.LoadEmbeddingsByProfileBackgroundJob(profileId, userId, groupTaskId, subTasksPos);

            var dataPacksCount = (await _workspaceAddDataPacksRepository.GetAsync(e => e.WorkspaceId == workspaceId)).Count();

            var algorithm = await _abstractAlgorithmsRepository.FindAsync(algorithmId);

            var algorithmTypeId = algorithm.TypeId;

            if (algorithmTypeId == ClusterizationAlgorithmTypes.KMeans)
            {
                await _kMeansService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + dataPacksCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.OneCluster)
            {
                await _oneClusterService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + dataPacksCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.DBSCAN)
            {
                await _dbSCANService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + dataPacksCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.SpectralClustering)
            {
                await _spectralClusteringService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + dataPacksCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.GaussianMixture)
            {
                await _gaussianMixtureService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + dataPacksCount);
            }
            else
            {
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist]);
                await _tasksService.ChangeParentTaskState(groupTaskId, TaskStates.Error);
            }
        }

        public async Task<int> FastClusteringFull(FullFastClusteringRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var workflow = (await _fastClusteringWorkflowsRepository.GetAsync(e => e.OwnerId == userId)).FirstOrDefault();
            if (workflow == null) throw new HttpException(_localizer[ErrorMessagePatterns.FastClusteringWorkflowNotFound], HttpStatusCode.NotFound);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.Clustering],
                SubTasksCount = 4,
                IsGroupTask = true
            };
            var groupTaskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            #region addWorkspace
            var createSubTask1 = new CreateSubTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingWorkspace],
                GroupTaskId = groupTaskId,
                Position = 1
            };
            var taskId1 = await _tasksService.CreateSubTaskWithUserId(createSubTask1);


            await _tasksService.ChangeTaskState(taskId1, TaskStates.Process);

            int workspaceId;
            string newTitle;
            string newDescription;
            try
            {
                var addWorkspaceRequest = new AddClusterizationWorkspaceRequest()
                {
                    ChangingType = ChangingTypes.OnlyOwner,
                    VisibleType = ChangingTypes.OnlyOwner,
                    TypeId = ClusterizationTypes.External
                };


                if (request.Title == null || request.Title == "") newTitle = "Fast clustering";
                else newTitle = request.Title;


                if (request.Description == null || request.Description == "") newDescription = "Fast clustering";
                else newDescription = request.Description;

                addWorkspaceRequest.Title = newTitle;
                addWorkspaceRequest.Description = newDescription;

                workspaceId = await _workspacesService.Add(addWorkspaceRequest);

                var origWorkspace = (await _workspacesRepository.GetAsync(e => e.Id == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.FastClusteringWorkflow)}")).FirstOrDefault();
                if (origWorkspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

                origWorkspace.FastClusteringWorkflowId = workflow.Id;
                await _fastClusteringWorkflowsRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskDescription(taskId1, ex.Message);
                await _tasksService.ChangeTaskState(taskId1, TaskStates.Error);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                throw ex;
            }
            await _tasksService.ChangeTaskPercent(taskId1, 100f);
            await _tasksService.ChangeTaskState(taskId1, TaskStates.Completed);
            #endregion

            #region addProfile
            var createSubTask2 = new CreateSubTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.AddingProfiles],
                GroupTaskId = groupTaskId,
                Position = 2
            };
            var taskId2 = await _tasksService.CreateSubTaskWithUserId(createSubTask2);


            await _tasksService.ChangeTaskState(taskId2, TaskStates.Process);

            int profileId;
            try
            {
                var addProfileRequest = new AddClusterizationProfileRequest()
                {
                    ChangingType = ChangingTypes.OnlyOwner,
                    VisibleType = ChangingTypes.OnlyOwner,
                    WorkspaceId = workspaceId,
                    AlgorithmId = request.AlgorithmId,
                    EmbeddingModelId = request.EmbeddingModelId,
                    DimensionCount = request.DimensionCount,
                    DRTechniqueId = request.DRTechniqueId,
                };

                profileId = await _profilesService.Add(addProfileRequest);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskDescription(taskId2, ex.Message);
                await _tasksService.ChangeTaskState(taskId2, TaskStates.Error);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                throw ex;
            }
            await _tasksService.ChangeTaskPercent(taskId2, 100f);
            await _tasksService.ChangeTaskState(taskId2, TaskStates.Completed);

            #endregion

            _backgroundJobClient.Enqueue(() => FastClusteringFullBackgroundJob(request, userId, groupTaskId, 3, newTitle, newDescription, profileId, workspaceId));

            return profileId;
        }
        public async Task FastClusteringFullBackgroundJob(FullFastClusteringRequest request, string userId, string groupTaskId, int subTasksPos, string newTitle, string newDescription, int profileId, int workspaceId)
        {
            var stateId = await _tasksService.GetTaskStateId(groupTaskId);
            if (stateId != TaskStates.Wait) return;

            var objectsList = new List<ExternalObjectModelDTO>(request.Texts.Count);

            for (int i = 0; i < request.Texts.Count; i++)
            {
                objectsList.Add(new ExternalObjectModelDTO()
                {
                    Id = i + "",
                    Text = request.Texts.ElementAt(i)
                });
            }

            var addDataRequest = new AddExternalDataWithoutFileRequest()
            {
                ChangingType = ChangingTypes.OnlyOwner,
                VisibleType = ChangingTypes.OnlyOwner,
                Description = newDescription,
                Title = newTitle,
                WorkspaceId = workspaceId,
                ObjectsList = objectsList
            };

            await _externalDataObjectsPacksService.LoadExternalDataAndAddToWorkspaceBackgroundJob(addDataRequest, userId, groupTaskId, subTasksPos);

            await _embeddingsLoadingService.LoadEmbeddingsByProfileBackgroundJob(profileId, userId, groupTaskId, subTasksPos+2);

            var algorithm = await _abstractAlgorithmsRepository.FindAsync(request.AlgorithmId);

            var algorithmTypeId = algorithm.TypeId;

            if (algorithmTypeId == ClusterizationAlgorithmTypes.KMeans)
            {
                await _kMeansService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos+3);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.OneCluster)
            {
                await _oneClusterService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + 3);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.DBSCAN)
            {
                await _dbSCANService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + 3);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.SpectralClustering)
            {
                await _spectralClusteringService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + 3);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.GaussianMixture)
            {
                await _gaussianMixtureService.ClusterDataBackgroundJob(profileId, groupTaskId, userId, subTasksPos + 3);
            }
            else
            {
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist]);
                await _tasksService.ChangeParentTaskState(groupTaskId, TaskStates.Error);
            }
        }


        public async Task<int> GetFastClusteringWorkflowId()
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var workflow = (await _fastClusteringWorkflowsRepository.GetAsync(e => e.OwnerId == userId)).FirstOrDefault();
            if (workflow == null) throw new HttpException(_localizer[ErrorMessagePatterns.FastClusteringWorkflowNotFound], HttpStatusCode.NotFound);

            return workflow.Id;
        }
        public async Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetWorkspaces(PageParameters pageParameters)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var fastClustering = (await _fastClusteringWorkflowsRepository.GetAsync(e => e.OwnerId == userId)).FirstOrDefault();
            if (fastClustering == null) throw new HttpException(_localizer[ErrorMessagePatterns.FastClusteringWorkflowNotFound], HttpStatusCode.NotFound);

            var workspaces = (await _workspacesRepository.GetAsync(e => e.FastClusteringWorkflowId == fastClustering.Id,
                                                                   includeProperties: $"{nameof(ClusterizationWorkspace.Type)},{nameof(ClusterizationWorkspace.Owner)}",
                                                                   pageParameters: pageParameters));

            return _mapper.Map<ICollection<SimpleClusterizationWorkspaceDTO>>(workspaces);
        }

        public async Task<ICollection<QuotasCalculationDTO>> CalculateInitialProfileQuotas(FastClusteringProcessRequest request)
        {
            var quotasCalculationList = new List<QuotasCalculationDTO>();

            var privateProfileQuotasType = await _quotasTypesRepository.FindAsync(QuotasTypes.PrivateProfiles);
            quotasCalculationList.Add(new QuotasCalculationDTO()
            {
                Count = 1,
                Type = _mapper.Map<QuotasTypeDTO>(privateProfileQuotasType)
            });

            var embeddingsQuotasType = await _quotasTypesRepository.FindAsync(QuotasTypes.Embeddings);
            
            var embeddingModel = await _embeddingModelsRepository.FindAsync(request.EmbeddingModelId);
            if (embeddingModel == null) throw new HttpException(_localizer[ErrorMessagePatterns.EmbeddingModelNotFound], HttpStatusCode.NotFound);

            var workspace = await _workspacesRepository.FindAsync(request.WorkspaceId);
            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            quotasCalculationList.Add(new QuotasCalculationDTO()
            {
                Count = workspace.EntitiesCount * embeddingModel.MaxInputCount,
                Type = _mapper.Map<QuotasTypeDTO>(embeddingsQuotasType)
            });

            var clusteringQuotasType = await _quotasTypesRepository.FindAsync(QuotasTypes.Clustering);
            var algorithm = await _abstractAlgorithmsRepository.FindAsync(request.AlgorithmId);
            var clusterQuotasCount = await _generalClusterizationAlgorithmService.CalculateQuotasCount(algorithm.TypeId, workspace.EntitiesCount, request.DimensionCount);

            quotasCalculationList.Add(new QuotasCalculationDTO()
            {
                Count = clusterQuotasCount,
                Type = _mapper.Map<QuotasTypeDTO>(clusteringQuotasType)
            });

            return quotasCalculationList;
        }
    }
}
