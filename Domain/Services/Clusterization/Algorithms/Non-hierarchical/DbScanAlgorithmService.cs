using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Hangfire;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Resources.Types;
using System.Net;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Accord.MachineLearning;
using Domain.HelpModels;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using Domain.ClusteringAlgorithms;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class DbScanAlgorithmService : IAbstractClusterizationAlgorithmService<AddDBScanAlgorithmDTO, DBScanAlgorithmDTO>
    {
        private readonly IRepository<DBScanAlgorithm> repository;
        private readonly IRepository<ClusterizationProfile> profile_repository;
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<Cluster> clusters_repository;
        private readonly IRepository<ClusterizationTilesLevel> tilesLevel_repository;
        private readonly IRepository<DimensionalityReductionValue> drValues_repository;

        private readonly IClusterizationTilesService tilesService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;
        private readonly IDimensionalityReductionValuesService drValues_service;

        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;

        private readonly IClusterizationAlgorithmsHelpService helpService;

        private const int TILES_COUNT = 16;

        public DbScanAlgorithmService(IRepository<DBScanAlgorithm> repository,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMapper mapper,
                                      IRepository<ClusterizationProfile> profile_repository,
                                      IRepository<ClusterizationEntity> entities_repository,
                                      IClusterizationTilesService tilesService,
                                      IRepository<Cluster> clusters_repository,
                                      IBackgroundJobClient backgroundJobClient,
                                      IMyTasksService taskService,
                                      IRepository<ClusterizationTilesLevel> tilesLevel_repository,
                                      IDimensionalityReductionValuesService drValues_service,
                                      IRepository<DimensionalityReductionValue> drValues_repository,
                                      IClusterizationAlgorithmsHelpService helpService)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.profile_repository = profile_repository;
            this.entities_repository = entities_repository;
            this.tilesService = tilesService;
            this.clusters_repository = clusters_repository;
            this.backgroundJobClient = backgroundJobClient;
            this.taskService = taskService;
            this.tilesLevel_repository = tilesLevel_repository;
            this.drValues_service = drValues_service;
            this.drValues_repository = drValues_repository;
            this.helpService = helpService;
        }
        public async Task AddAlgorithm(AddDBScanAlgorithmDTO model)
        {
            var list = await repository.GetAsync(c => c.Epsilon == model.Epsilon && c.MinimumPointsPerCluster == model.MinimumPointsPerCluster);

            if (list.Any()) throw new HttpException(localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new DBScanAlgorithm()
            {
                Epsilon = model.Epsilon,
                MinimumPointsPerCluster = model.MinimumPointsPerCluster,
                TypeId = ClusterizationAlgorithmTypes.DBScan
            };

            await repository.AddAsync(newAlg);
            await repository.SaveChangesAsync();
        }

        public async Task ClusterData(int profileId)
        {
            backgroundJobClient.Enqueue(() => ClusterDataBackgroundJob(profileId));
        }

        public async Task ClusterDataBackgroundJob(int profileId)
        {
            var taskId = await taskService.CreateTask("Кластеризація (DBSCAN)");
            await taskService.ChangeTaskState(taskId, TaskStates.Process);
            try
            {
                var profile = (await profile_repository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.TilesLevels)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.DimensionType)}")).FirstOrDefault();

                if (profile == null || profile.Algorithm.TypeId != ClusterizationAlgorithmTypes.DBScan) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);

                var clusterAlgorithm = (await repository.GetAsync(e => e.Id == profile.AlgorithmId)).FirstOrDefault();

                for (int i = 0; i < profile.Clusters.Count(); i++)
                {
                    var id = profile.Clusters.ElementAt(i).Id;
                    var clusterForDelete = (await clusters_repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(Cluster.Entities)},{nameof(Cluster.DisplayedPoints)},{nameof(Cluster.Profile)}")).FirstOrDefault();

                    clusters_repository.Remove(clusterForDelete);
                }
                for (int i = 0; i < profile.TilesLevels.Count(); i++)
                {
                    var id = profile.TilesLevels.ElementAt(i).Id;
                    await tilesService.FullRemoveTilesLevel(id);
                }

                profile.Clusters.Clear();
                profile.TilesLevels.Clear();

                if (profile.DimensionalityReductionTechniqueId != DimensionalityReductionTechniques.JSL)
                {
                    if (profile.DimensionCount == 1536)
                    {
                        await drValues_service.AddEmbeddingValues(profile.WorkspaceId, profile.DimensionalityReductionTechniqueId, 2);
                    }
                    else
                    {
                        await drValues_service.AddEmbeddingValues(profile.WorkspaceId, profile.DimensionalityReductionTechniqueId, profile.DimensionCount);
                    }
                }
                await taskService.ChangeTaskPercent(taskId, 30f);

                var entitiesHelpModels = await helpService.CreateHelpModels(profile);
                var clusters = await DBSCAN(entitiesHelpModels, profile.DimensionalityReductionTechniqueId, profile.DimensionCount, clusterAlgorithm.Epsilon, clusterAlgorithm.MinimumPointsPerCluster);

                foreach (var cluster in clusters)
                {
                    cluster.ProfileId = profile.Id;
                    await clusters_repository.AddAsync(cluster);
                }
                await taskService.ChangeTaskPercent(taskId, 60f);

                List<TileGeneratingHelpModel> helpModels = new List<TileGeneratingHelpModel>(entitiesHelpModels.Count());

                foreach (var entityHelpModel in entitiesHelpModels)
                {
                    helpModels.Add(new TileGeneratingHelpModel()
                    {
                        Entity = entityHelpModel.Entity,
                        Cluster = clusters.Where(e => e.Entities.Contains(entityHelpModel.Entity)).FirstOrDefault()
                    });
                }
                var tilesLevel = new ClusterizationTilesLevel()
                {
                    Profile = profile,
                    TileCount = TILES_COUNT,
                    Z = 0
                };

                var tiles = await tilesService.GenerateOneLevelTiles(helpModels, TILES_COUNT, 0, tilesLevel, profile.DimensionalityReductionTechniqueId);
                tilesLevel.Tiles = tiles;

                await tilesLevel_repository.AddAsync(tilesLevel);

                profile.MaxTileLevel = 0;
                profile.MinTileLevel = 0;

                profile.Tiles = tiles;
                profile.IsCalculated = true;

                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
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
                    newCluster.Entities.Add(helpModels[index].Entity);
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

        public async Task<ICollection<DBScanAlgorithmDTO>> GetAllAlgorithms()
        {
            var algorithms = await repository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}");

            return mapper.Map<ICollection<DBScanAlgorithmDTO>>(algorithms);
        }
    }
}