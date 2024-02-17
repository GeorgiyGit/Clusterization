using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class OneClusterAlgorithmService : IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmDTO, OneClusterAlgorithmDTO>
    {
        private readonly IRepository<OneClusterAlgorithm> repository;
        private readonly IRepository<ClusterizationProfile> profile_repository;
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<Cluster> clusters_repository;
        private readonly IRepository<ClusterizationTilesLevel> tilesLevel_repository;

        private readonly IClusterizationTilesService tilesService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;
        private readonly IDimensionalityReductionValuesService drValues_service;

        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;

        private const int TILES_COUNT = 16;

        public OneClusterAlgorithmService(IRepository<OneClusterAlgorithm> repository,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMapper mapper,
                                      IRepository<ClusterizationProfile> profile_repository,
                                      IRepository<ClusterizationEntity> entities_repository,
                                      IClusterizationTilesService tilesService,
                                      IRepository<Cluster> clusters_repository,
                                      IBackgroundJobClient backgroundJobClient,
                                      IMyTasksService taskService,
                                      IRepository<ClusterizationTilesLevel> tilesLevel_repository,
                                      IDimensionalityReductionValuesService drValues_service)
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
        }

        public async Task AddAlgorithm(AddOneClusterAlgorithmDTO model)
        {
            var list = await repository.GetAsync(c => c.ClusterColor == model.ClusterColor);

            if (list.Any()) throw new HttpException(localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new OneClusterAlgorithm()
            {
                ClusterColor = model.ClusterColor,
                TypeId = ClusterizationAlgorithmTypes.OneCluster
            };

            await repository.AddAsync(newAlg);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<OneClusterAlgorithmDTO>> GetAllAlgorithms()
        {
            var algorithms = await repository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}");

            return mapper.Map<ICollection<OneClusterAlgorithmDTO>>(algorithms);
        }

        public async Task ClusterData(int profileId)
        {
            await WorkspaceVerification(profileId);

            backgroundJobClient.Enqueue(() => ClusterDataBackgroundJob(profileId));
        }
        public async Task WorkspaceVerification(int profileId)
        {
            var profile = (await profile_repository.GetAsync(e => e.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();
            if (profile == null || !profile.Workspace.IsAllDataEmbedded) throw new HttpException(localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);
        }
        public async Task ClusterDataBackgroundJob(int profileId)
        {
            var taskId = await taskService.CreateTask("Кластеризація (один кластер)");
            await taskService.ChangeTaskState(taskId, TaskStates.Process);
            try
            {
                var profile = (await profile_repository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.TilesLevels)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)}")).FirstOrDefault();

                if (profile == null || profile.Algorithm.TypeId != ClusterizationAlgorithmTypes.OneCluster) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);

                var clusterColor = (profile.Algorithm as OneClusterAlgorithm)?.ClusterColor;

                var clusterizationEntities = (await entities_repository.GetAsync(e => e.WorkspaceId == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationEntity.EmbeddingData)}")).ToList();

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

                var cluster = new Cluster()
                {
                    Color = clusterColor,
                    Profile = profile,
                    Entities = clusterizationEntities
                };
                await clusters_repository.AddAsync(cluster);

                profile.Clusters.Add(cluster);

                if (profile.DimensionalityReductionTechniqueId != DimensionalityReductionTechniques.JSL)
                {
                    await drValues_service.AddEmbeddingValues(profile.WorkspaceId, profile.DimensionalityReductionTechniqueId, 2);
                }
                await taskService.ChangeTaskPercent(taskId, 50f);

                List<TileGeneratingHelpModel> helpModels = new List<TileGeneratingHelpModel>(clusterizationEntities.Count());

                foreach(var entity in clusterizationEntities)
                {
                    helpModels.Add(new TileGeneratingHelpModel()
                    {
                        Entity = entity,
                        Cluster = cluster
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
    }
}
