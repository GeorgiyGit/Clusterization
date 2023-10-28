using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
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

        private readonly IClusterizationTilesService tilesService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;

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
                                      IMyTasksService taskService)
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
            backgroundJobClient.Enqueue(() => ClusterDataBackgroundJob(profileId));
        }

        public async Task ClusterDataBackgroundJob(int profileId)
        {
            var taskId = await taskService.CreateTask("Кластеризація (один кластер)");
            await taskService.ChangeTaskState(taskId, TaskStates.Process);
            try
            {
                var profile = (await profile_repository.GetAsync(c => c.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();

                if (profile == null || profile.Algorithm.TypeId != ClusterizationAlgorithmTypes.OneCluster) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);

                var clusterColor = (profile.Algorithm as OneClusterAlgorithm)?.ClusterColor;

                var clusterizationEntities = (await entities_repository.GetAsync(e => e.WorkspaceId == profile.WorkspaceId, includeProperties: $"{nameof(ClusterizationEntity.EmbeddingData)}")).ToList();

                for (int i = 0; i < profile.Clusters.Count(); i++)
                {
                    clusters_repository.Remove(profile.Clusters.ElementAt(i));
                }

                var cluster = new Cluster()
                {
                    Color = clusterColor,
                    Profile = profile,
                    Entities = clusterizationEntities
                };
                await clusters_repository.AddAsync(cluster);

                profile.Clusters.Add(cluster);

                var tiles = await tilesService.GenerateOneLevelTiles(clusterizationEntities, TILES_COUNT, 0);

                profile.MaxTileLevel = 0;
                profile.MinTileLevel = 0;

                profile.Tiles = tiles;
                profile.IsCalculated = true;

                await profile_repository.SaveChangesAsync();

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
