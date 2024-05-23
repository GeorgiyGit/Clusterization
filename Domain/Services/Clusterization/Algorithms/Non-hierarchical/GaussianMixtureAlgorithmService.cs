using AutoMapper;
using Domain.ClusteringAlgorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Net;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.Resources.Localization.Tasks;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Customers;
using Domain.Entities.Clusterization.Displaying;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Entities.DataObjects;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Entities.Embeddings;
using Domain.Interfaces.Other;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.Clusterization.Profiles;
using Domain.Resources.Types.Clusterization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Resources.Types.Tasks;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs;
using Domain.Entities.Clusterization.Algorithms;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class GaussianMixtureAlgorithmService : AbstractAlgorithmService<GaussianMixtureAlgorithm,GaussianMixtureAlgorithmDTO>, IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmRequest, GaussianMixtureAlgorithmDTO>
    {
        private readonly IRepository<ClusterizationWorkspace> _workspaceRepository;

        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IDimensionalityReductionService _dimensionalityReductionService;

        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IQuotasControllerService _quotasControllerService;
        private readonly IUserService _userService;

        public GaussianMixtureAlgorithmService(IRepository<GaussianMixtureAlgorithm> algorithmsRepository,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMapper mapper,
                                      IRepository<ClusterizationProfile> profilesRepository,
                                      IClusterizationTilesService tilesService,
                                      IRepository<Cluster> clustersRepository,
                                      IBackgroundJobClient backgroundJobClient,
                                      IMyTasksService tasksService,
                                      IRepository<ClusterizationTilesLevel> tilesLevelRepository,
                                      IDimensionalityReductionService dimensionalityReductionService,
                                      IStringLocalizer<TaskTitles> tasksLocalizer,
                                      IQuotasControllerService quotasControllerService,
                                      IUserService userService,
                                      IRepository<ClusterizationWorkspace> workspaceRepository,
                                      IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository,
                                      IRepository<DimensionEmbeddingObject> dimensionEmbeddingObjectsRepository,
                                      IRepository<DisplayedPoint> displayedPointsRepository) : base(clustersRepository,
                                                                                                                        tilesService,
                                                                                                                        tilesLevelRepository,
                                                                                                                        algorithmsRepository,
                                                                                                                        mapper,
                                                                                                                        localizer,
                                                                                                                        profilesRepository,
                                                                                                                        embeddingObjectsGroupsRepository,
                                                                                                                        dimensionEmbeddingObjectsRepository,
                                                                                                                        displayedPointsRepository)
        {
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _tasksLocalizer = tasksLocalizer;
            _quotasControllerService = quotasControllerService;
            _userService = userService;
            _workspaceRepository = workspaceRepository;
            _dimensionalityReductionService = dimensionalityReductionService;
        }
        public async Task AddAlgorithm(AddGaussianMixtureAlgorithmRequest model)
        {
            var list = await _algorithmsRepository.GetAsync(c => c.NumberOfComponents == model.NumberOfComponents);

            if (list.Any()) throw new HttpException(_localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new GaussianMixtureAlgorithm()
            {
                NumberOfComponents = model.NumberOfComponents,
                TypeId = ClusterizationAlgorithmTypes.GaussianMixture
            };

            await _algorithmsRepository.AddAsync(newAlg);
            await _algorithmsRepository.SaveChangesAsync();
        }
        public async Task<ICollection<GaussianMixtureAlgorithmDTO>> GetAlgorithms(PageParameters pageParameters)
        {
            var algorithms = await _algorithmsRepository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}",
                                                                  orderBy: order => order.OrderBy(e => e.NumberOfComponents),
                                                                  pageParameters: pageParameters);

            return _mapper.Map<ICollection<GaussianMixtureAlgorithmDTO>>(algorithms);
        }
        public async Task<int> CalculateQuotasCount(int dataObjectsCount, int dimensionCount)
        {
            return (int)((double)dataObjectsCount * (double)dimensionCount * (1 + Math.Sqrt(dimensionCount)));
        }
        public async Task ClusterData(int profileId)
        {
            await WorkspaceVerification(profileId);

            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                EntityId = profileId + "",
                EntityType = TaskEntityTypes.ClusterizationProfile,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.ClusterizationGaussianMixture],
                IsGroupTask = true,
                SubTasksCount = 3
            };
            var taskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            _backgroundJobClient.Enqueue(() => ClusterDataBackgroundJob(profileId, taskId, userId));
        }
        public async Task ClusterDataBackgroundJob(int profileId, string taskId, string userId)
        {
            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            var profile = (await _profilesRepository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.TilesLevels)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.EmbeddingModel)},{nameof(ClusterizationProfile.EmbeddingLoadingState)},{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();
            if (profile == null || profile.Algorithm.TypeId != ClusterizationAlgorithmTypes.GaussianMixture)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.ProfileNotFound]);
                return;
            };

            if (!profile.EmbeddingLoadingState.IsAllEmbeddingsLoaded)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotAllDataEmbedded]);
                return;
            }

            #region tasksCreating
            var taskOptions1 = new CreateSubTaskOptions()
            {
                Position = 1,
                GroupTaskId = taskId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.DimensionReduction],
                IsPercents = false
            };
            var subTaskId1 = await _tasksService.CreateSubTaskWithUserId(taskOptions1);

            var taskOptions2 = new CreateSubTaskOptions()
            {
                Position = 2,
                GroupTaskId = taskId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.Clustering],
                IsPercents = false
            };
            var subTaskId2 = await _tasksService.CreateSubTaskWithUserId(taskOptions2);

            var taskOptions3 = new CreateSubTaskOptions()
            {
                Position = 3,
                GroupTaskId = taskId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.TilesCreating],
                IsPercents = false
            };
            var subTaskId3 = await _tasksService.CreateSubTaskWithUserId(taskOptions3);
            #endregion

            var workspace = (await _workspaceRepository.GetAsync(e => e.Id == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)}")).FirstOrDefault();
            try
            {
                profile.IsInCalculation = true;
                workspace.IsProfilesInCalculation = true;
                await _workspaceRepository.SaveChangesAsync();

                var clusterAlgorithm = (await _algorithmsRepository.GetAsync(e => e.Id == profile.AlgorithmId)).FirstOrDefault();

                double quotasCount = await CalculateQuotasCount(profile.Workspace.EntitiesCount, profile.DimensionCount);

                var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Clustering, (int)quotasCount, Guid.NewGuid().ToString());

                if (!quotasResult)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                }

                await RemoveClusters(profile);

                var dataObjects = workspace.DataObjects;


                await _tasksService.ChangeTaskState(subTaskId1, TaskStates.Process);
                if (profile.DRTechniqueId != DimensionalityReductionTechniques.JSL)
                {
                    try
                    {
                        await _dimensionalityReductionService.AddEmbeddingValues(profile.WorkspaceId, profile.DRTechniqueId, profile.EmbeddingModelId, profile.DimensionCount);
                    }
                    catch (Exception ex)
                    {
                        await _tasksService.ChangeTaskState(subTaskId1, TaskStates.Error);
                        await _tasksService.ChangeTaskDescription(subTaskId1, ex.Message);
                        throw ex;
                    }
                }
                await _tasksService.ChangeTaskPercent(subTaskId1, 100f);
                await _tasksService.ChangeTaskState(subTaskId1, TaskStates.Completed);
                await _tasksService.ChangeTaskPercent(taskId, 30f);


                List<AddEmbeddingsWithDRHelpModel> entitiesHelpModels = new List<AddEmbeddingsWithDRHelpModel>();
                List<Cluster> clusters = new List<Cluster>();
                await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Process);
                try
                {
                    entitiesHelpModels = await CreateHelpModels(dataObjects.ToList(), profile.DRTechniqueId, profile.EmbeddingModelId, profile.WorkspaceId, profile.DimensionCount);
                    clusters = await GaussianMixture(entitiesHelpModels, profile.DRTechniqueId, profile.DimensionCount, clusterAlgorithm.NumberOfComponents);

                    foreach (var cluster in clusters)
                    {
                        cluster.ProfileId = profile.Id;
                        await _clustersRepository.AddAsync(cluster);
                    }
                }
                catch (Exception ex)
                {
                    await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Error);
                    await _tasksService.ChangeTaskDescription(subTaskId2, ex.Message);
                    throw ex;
                }
                await _tasksService.ChangeTaskPercent(subTaskId2, 100f);
                await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Completed);
                await _tasksService.ChangeTaskPercent(taskId, 60f);

                await _tasksService.ChangeTaskState(subTaskId3, TaskStates.Process);
                try
                {
                    List<TileGeneratingHelpModel> helpModels = new List<TileGeneratingHelpModel>(entitiesHelpModels.Count());

                    foreach (var entityHelpModel in entitiesHelpModels)
                    {
                        helpModels.Add(new TileGeneratingHelpModel()
                        {
                            DataObject = entityHelpModel.DataObject,
                            Cluster = clusters.Where(e => e.DataObjects.Contains(entityHelpModel.DataObject)).FirstOrDefault()
                        });
                    }

                    await AddTiles(profile, helpModels);
                }
                catch (Exception ex)
                {
                    await _tasksService.ChangeTaskState(subTaskId3, TaskStates.Error);
                    await _tasksService.ChangeTaskDescription(subTaskId3, ex.Message);
                    throw ex;
                }
                await _tasksService.ChangeTaskPercent(subTaskId3, 100f);
                await _tasksService.ChangeTaskState(subTaskId3, TaskStates.Completed);
                await _tasksService.ChangeTaskPercent(taskId, 90f);


                profile.IsInCalculation = false;
                await _profilesRepository.SaveChangesAsync();
                workspace.IsProfilesInCalculation = await ReviewWorkspaceIsProfilesInCalculation(workspace.Id);

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                profile.IsInCalculation = false;
                workspace.IsProfilesInCalculation = await ReviewWorkspaceIsProfilesInCalculation(workspace.Id);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }
        
        #region algorithm
        public async Task<List<Cluster>> GaussianMixture(List<AddEmbeddingsWithDRHelpModel> helpModels, string drTechniqueId, int dimensionsCount, int numberOfComponents)
        {
            var values = helpModels.Select(e => e.DataPoints).ToArray();

            MyGaussianMixture gmm = new MyGaussianMixture(numberOfComponents, 100, 10);

            int iterations= gmm.Cluster(values);

            int[] labels = gmm.PredictLabels(values);

            var resultedClusters = new List<Cluster>();
            var tempClusters = new List<TempCluster>();

            for (int i = 0; i < labels.Length; i++)
            {
                var cluster = tempClusters.Where(e => e.Id == labels[i]).FirstOrDefault();

                if (cluster == null)
                {
                    cluster = new TempCluster()
                    {
                        Id = labels[i]
                    };
                    tempClusters.Add(cluster);
                }
                cluster.EntityIds.Add(i);
            }
            foreach (var tempCluster in tempClusters)
            {
                var newCluster = new Cluster()
                {
                    Color = GetRandomColor()
                };
                resultedClusters.Add(newCluster);
                foreach (int index in tempCluster.EntityIds)
                {
                    newCluster.DataObjects.Add(helpModels[index].DataObject);
                    newCluster.ChildElementsCount++;
                }
            }

            return resultedClusters;
        }

        // Helper function to generate a random color
        static string GetRandomColor()
        {
            Random random = new Random();
            return $"#{random.Next(0x1000000):X6}";
        }
        #endregion
    }
}
