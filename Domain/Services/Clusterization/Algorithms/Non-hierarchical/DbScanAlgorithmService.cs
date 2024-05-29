using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.Entities.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Hangfire;
using Microsoft.Extensions.Localization;
using Domain.Exceptions;
using Domain.Resources.Types;
using System.Net;
using Domain.HelpModels;
using Domain.ClusteringAlgorithms;
using Domain.Resources.Localization.Tasks;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Customers;
using Domain.Entities.Clusterization.Displaying;
using Domain.Interfaces.Clusterization.Displaying;
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
using Domain.Entities.Clusterization.Algorithms;
using Domain.DTOs;
using Microsoft.Extensions.Caching.Distributed;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class DBSCANAlgorithmService : AbstractAlgorithmService<DBSCANAlgorithm, DBSCANAlgorithmDTO>, IAbstractClusterizationAlgorithmService<AddDBSCANAlgorithmRequest, DBSCANAlgorithmDTO>
    {
        private readonly IRepository<ClusterizationWorkspace> _workspaceRepository;

        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IDimensionalityReductionService _dimensionalityReductionService;

        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IQuotasControllerService _quotasControllerService;
        private readonly IUserService _userService;

        public DBSCANAlgorithmService(IRepository<DBSCANAlgorithm> algorithmsRepository,
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
                                      IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository,
                                      IRepository<DimensionEmbeddingObject> dimensionEmbeddingObjectsRepository,
                                      IRepository<ClusterizationWorkspace> workspaceRepository,
                                      IRepository<DisplayedPoint> displayedPointsRepository,
                                      IDistributedCache distributedCache) : base(clustersRepository,
                                          tilesService,
                                          tilesLevelRepository,
                                          algorithmsRepository,
                                          mapper,
                                          localizer,
                                          profilesRepository,
                                          embeddingObjectsGroupsRepository,
                                          dimensionEmbeddingObjectsRepository,
                                          displayedPointsRepository,
                                          distributedCache)
        {
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _tasksLocalizer = tasksLocalizer;
            _quotasControllerService = quotasControllerService;
            _userService = userService;
            _dimensionalityReductionService = dimensionalityReductionService;
            _workspaceRepository = workspaceRepository;
        }
        public async Task<ICollection<DBSCANAlgorithmDTO>> GetAlgorithms(PageParameters pageParameters)
        {
            var algorithms = await _algorithmsRepository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}",
                                                                  orderBy: order => order.OrderBy(e => e.Epsilon),
                                                                  pageParameters: pageParameters);

            return _mapper.Map<ICollection<DBSCANAlgorithmDTO>>(algorithms);
        }
        public async Task AddAlgorithm(AddDBSCANAlgorithmRequest model)
        {
            var list = await _algorithmsRepository.GetAsync(c => c.Epsilon == model.Epsilon && c.MinimumPointsPerCluster == model.MinimumPointsPerCluster);

            if (list.Any()) throw new HttpException(_localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new DBSCANAlgorithm()
            {
                Epsilon = model.Epsilon,
                MinimumPointsPerCluster = model.MinimumPointsPerCluster,
                TypeId = ClusterizationAlgorithmTypes.DBSCAN
            };

            await _algorithmsRepository.AddAsync(newAlg);
            await _algorithmsRepository.SaveChangesAsync();
        }

        public async Task<int> CalculateQuotasCount(int dataObjectsCount, int dimensionCount)
        {
            return (int)((double)dataObjectsCount * (double)dimensionCount / 2d);
        }

        public async Task ClusterData(int profileId)
        {
            await WorkspaceVerification(profileId);

            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);


            var createTaskOptions = new CreateMainTaskOptions()
            {
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.ClusterizationDBSCAN],
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
                Title = _tasksLocalizer[TaskTitlesPatterns.Clustering],
                IsPercents = false,
                ClusterizationProfileId = profileId
            };
            var subTaskId2 = await _tasksService.CreateSubTaskWithUserId(taskOptions2);

            var taskOptions3 = new CreateSubTaskOptions()
            {
                Position = 3,
                GroupTaskId = groupTaskId,
                CustomerId = userId,
                Title = _tasksLocalizer[TaskTitlesPatterns.TilesCreating],
                IsPercents = false,
                ClusterizationProfileId = profileId
            };
            var subTaskId3 = await _tasksService.CreateSubTaskWithUserId(taskOptions3);
            #endregion

            _backgroundJobClient.Enqueue(() => ClusterDataBackgroundJob(profileId, groupTaskId, userId, new List<string>() { subTaskId1, subTaskId2, subTaskId3 }));
        }
        public async Task ClusterDataBackgroundJob(int profileId, string groupTaskId, string userId, List<string> subTaskIds)
        {
            await _tasksService.ChangeTaskState(groupTaskId, TaskStates.Process);

            var profile = (await _profilesRepository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.TilesLevels)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.EmbeddingModel)},{nameof(ClusterizationProfile.EmbeddingLoadingState)},{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();
            if (profile == null || profile.Algorithm.TypeId != ClusterizationAlgorithmTypes.DBSCAN)
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
                var subTaskId3 = subTaskIds[2];

                profile.IsInCalculation = true;
                workspace.IsProfilesInCalculation = true;
                await _workspaceRepository.SaveChangesAsync();
                
                var clusterAlgorithm = (await _algorithmsRepository.GetAsync(e => e.Id == profile.AlgorithmId)).FirstOrDefault();

                double quotasCount = await CalculateQuotasCount(profile.Workspace.EntitiesCount, profile.DimensionCount);

                var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Clustering, (int)quotasCount,Guid.NewGuid().ToString());

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
                await _tasksService.ChangeTaskPercent(groupTaskId, 30f);


                List<AddEmbeddingsWithDRHelpModel> entitiesHelpModels = new List<AddEmbeddingsWithDRHelpModel>();
                List<Cluster> clusters = new List<Cluster>();
                await _tasksService.ChangeTaskState(subTaskId2, TaskStates.Process);
                try
                {
                    entitiesHelpModels = await CreateHelpModels(dataObjects.ToList(), profile.DRTechniqueId, profile.EmbeddingModelId, profile.WorkspaceId, profile.DimensionCount);
                    clusters = await DBSCAN(entitiesHelpModels, profile.DRTechniqueId, profile.DimensionCount, clusterAlgorithm.Epsilon, clusterAlgorithm.MinimumPointsPerCluster);

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
                await _tasksService.ChangeTaskPercent(groupTaskId, 60f);

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
                await _tasksService.ChangeTaskPercent(groupTaskId, 90f);


                profile.IsInCalculation = false;
                await _profilesRepository.SaveChangesAsync();
                workspace.IsProfilesInCalculation = await ReviewWorkspaceIsProfilesInCalculation(workspace.Id);

                await RemoveCache(profileId);

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
        
        #region algorithm
        public async Task<List<Cluster>> DBSCAN(List<AddEmbeddingsWithDRHelpModel> helpModels, string drTechniqueId, int dimensionsCount, double epsilonPercent, int minimumPointsPerCluster)
        {
            var values = helpModels.Select(e => e.DataPoints).ToArray();

            double maxLength = 0;

            var epsilon = FindEpsilon(values, minimumPointsPerCluster);

            double newEpsilon = epsilon / 50d * epsilonPercent;

            MyDBSCANClustering dbSCAN = new MyDBSCANClustering(newEpsilon, minimumPointsPerCluster);

            int[] labels = dbSCAN.Cluster(values);

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
            foreach(var tempCluster in tempClusters)
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
        public static double FindEpsilon(double[][] values, int minPts)
        {
            List<double> distances = new List<double>();

            // Calculate distances to k-nearest neighbors for each point
            for (int i = 0; i < values.Length; i++)
            {
                double[] point = values[i];
                List<double> pointDistances = new List<double>();

                for (int j = 0; j < values.Length; j++)
                {
                    if (i != j)
                    {
                        double distance = CalculateDistance(point, values[j]);
                        pointDistances.Add(distance);
                    }
                }

                pointDistances.Sort();
                distances.AddRange(pointDistances.Take(minPts));
            }

            // Sort the distances and find an epsilon value
            distances.Sort();
            int index = (int)(values.Length * 0.9); // Consider the distance at the 90th percentile
            return distances[index];
        }

        private static double CalculateDistance(double[] pointA, double[] pointB)
        {
            double sum = 0;
            for (int i = 0; i < pointA.Length; i++)
            {
                sum += Math.Pow(pointA[i] - pointB[i], 2);
            }
            return Math.Sqrt(sum);
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