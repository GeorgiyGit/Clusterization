using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Displaying;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Net;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Entities.DataObjects;
using Domain.Entitie.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Entities.Embeddings;
using Domain.Interfaces.Other;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.Clusterization.Profiles;
using Domain.Resources.Types.Clusterization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Resources.Types.Tasks;
using Domain.DTOs;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class OneClusterAlgorithmService : AbstractAlgorithmService<OneClusterAlgorithm,OneClusterAlgorithmDTO>,IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmRequest, OneClusterAlgorithmDTO>
    {
        private readonly IRepository<ClusterizationWorkspace> _workspaceRepository;

        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IDimensionalityReductionService _dimensionalityReductionService;

        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IQuotasControllerService _quotasControllerService;
        private readonly IUserService _userService;

        public OneClusterAlgorithmService(IRepository<OneClusterAlgorithm> algorithmsRepository,
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

        public async Task AddAlgorithm(AddOneClusterAlgorithmRequest model)
        {
            var list = await _algorithmsRepository.GetAsync(c => c.ClusterColor == model.ClusterColor);

            if (list.Any()) throw new HttpException(_localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new OneClusterAlgorithm()
            {
                ClusterColor = model.ClusterColor,
                TypeId = ClusterizationAlgorithmTypes.OneCluster
            };

            await _algorithmsRepository.AddAsync(newAlg);
            await _algorithmsRepository.SaveChangesAsync();
        }
        public async Task<ICollection<OneClusterAlgorithmDTO>> GetAlgorithms(PageParameters pageParameters)
        {
            var algorithms = await _algorithmsRepository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}",
                                                                  pageParameters: pageParameters);

            return _mapper.Map<ICollection<OneClusterAlgorithmDTO>>(algorithms);
        }
        public async Task<int> CalculateQuotasCount(int dataObjectsCount, int dimensionCount)
        {
            return (int)(1 + (double)dataObjectsCount / 5d);
        }
        public async Task ClusterData(int profileId)
        {
            await WorkspaceVerification(profileId);
            
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var createTaskOptions = new CreateMainTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.ClusterizationOneCluser],
                IsGroupTask = true,
                ClusterizationProfileId = profileId
            };
            var groupTaskId = await _tasksService.CreateMainTaskWithUserId(createTaskOptions);

            #region tasksCreating
            var taskOptions1 = new CreateSubTaskOptions()
            {
                Position = 1,
                GroupTaskId = groupTaskId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.DimensionReduction],
                IsPercents = false,
                ClusterizationProfileId = profileId
            };
            var subTaskId1 = await _tasksService.CreateSubTaskWithUserId(taskOptions1);

            var taskOptions2 = new CreateSubTaskOptions()
            {
                Position = 2,
                GroupTaskId = groupTaskId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.TilesCreating],
                IsPercents = false,
                ClusterizationProfileId = profileId
            };
            var subTaskId2 = await _tasksService.CreateSubTaskWithUserId(taskOptions2);
            #endregion

            _backgroundJobClient.Enqueue(() => ClusterDataBackgroundJob(profileId, groupTaskId, userId, new List<string> { subTaskId1, subTaskId2 }));
        }
        public async Task ClusterDataBackgroundJob(int profileId, string groupTaskId, string userId, List<string> subTaskIds)
        {
            await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Process);

            var profile = (await _profilesRepository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.TilesLevels)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.EmbeddingModel)},{nameof(ClusterizationProfile.EmbeddingLoadingState)},{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();
            if (profile == null || profile.Algorithm.TypeId != ClusterizationAlgorithmTypes.OneCluster)
            {
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.ProfileNotFound]);
                return;
            };

            if (!profile.EmbeddingLoadingState.IsAllEmbeddingsLoaded)
            {
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, _localizer[ErrorMessagePatterns.NotAllDataEmbedded]);
                return;
            }

            var workspace = (await _workspaceRepository.GetAsync(e => e.Id == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)}")).FirstOrDefault();
            try
            {
                var subTaskId1 = subTaskIds[0];
                var subTaskId2 = subTaskIds[1];

                profile.IsInCalculation = true;
                workspace.IsProfilesInCalculation = true;
                await _workspaceRepository.SaveChangesAsync();

                double quotasCount = await CalculateQuotasCount(profile.Workspace.EntitiesCount, profile.DimensionCount);

                var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Clustering, (int)quotasCount, Guid.NewGuid().ToString());

                if (!quotasResult)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                }
                var clusterColor = (profile.Algorithm as OneClusterAlgorithm)?.ClusterColor;

                await RemoveClusters(profile);

                var dataObjects = workspace.DataObjects;

                var cluster = new Cluster()
                {
                    Color = clusterColor,
                    Profile = profile,
                    DataObjects = dataObjects.ToList(),
                    ChildElementsCount = dataObjects.Count()
                };
                await _clustersRepository.AddAsync(cluster);

                profile.Clusters.Add(cluster);

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
                await _tasksService.ChangeTaskPercent(groupTaskId, 50f);

                await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Process);
                try
                {
                    List<TileGeneratingHelpModel> helpModels = new List<TileGeneratingHelpModel>(dataObjects.Count);

                    foreach (var dataObject in dataObjects)
                    {
                        helpModels.Add(new TileGeneratingHelpModel()
                        {
                            DataObject = dataObject,
                            Cluster = cluster
                        });
                    }

                    await AddTiles(profile, helpModels);
                }
                catch (Exception ex)
                {
                    await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Error);
                    await _tasksService.ChangeTaskDescription(subTaskId2, ex.Message);
                    throw ex;
                }
                await _tasksService.ChangeTaskPercent(subTaskId2, 100f);
                await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Completed);
                await _tasksService.ChangeTaskPercent(groupTaskId, 90f);


                profile.IsInCalculation = false;
                await _profilesRepository.SaveChangesAsync();
                workspace.IsProfilesInCalculation = await ReviewWorkspaceIsProfilesInCalculation(workspace.Id);

                await _tasksService.ChangeTaskPercent(groupTaskId, 100f);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                profile.IsInCalculation = false;
                workspace.IsProfilesInCalculation = await ReviewWorkspaceIsProfilesInCalculation(workspace.Id);
                await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(groupTaskId, ex.Message);
            }
        }
    }
}
