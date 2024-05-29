using AutoMapper;
using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.DataObjects;
using Domain.Entities.Embeddings;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class AbstractAlgorithmService<Algorithm,AlgorithmDTO> where Algorithm : class
    {
        protected readonly IRepository<Cluster> _clustersRepository;
        protected readonly IRepository<ClusterizationTilesLevel> _tilesLevelRepository;
        protected readonly IRepository<Algorithm> _algorithmsRepository;
        protected readonly IRepository<ClusterizationProfile> _profilesRepository;
        protected readonly IRepository<EmbeddingObjectsGroup> _embeddingObjectsGroupsRepository;
        protected readonly IRepository<DimensionEmbeddingObject> _dimensionEmbeddingObjectsRepository;
        protected readonly IRepository<DisplayedPoint> _displayedPointsRepository;
        protected readonly IDistributedCache _distributedCache;

        protected readonly IStringLocalizer<ErrorMessages> _localizer;

        protected readonly IClusterizationTilesService _tilesService;
        protected readonly IMapper _mapper;



        private const int TILES_COUNT = 16;
        public AbstractAlgorithmService(IRepository<Cluster> clustersRepository,
            IClusterizationTilesService tilesService,
            IRepository<ClusterizationTilesLevel> tilesLevelRepository,
            IRepository<Algorithm> algorithmsRepository,
            IMapper mapper,
            IStringLocalizer<ErrorMessages> localizer,
            IRepository<ClusterizationProfile> profilesRepository,
            IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository,
            IRepository<DimensionEmbeddingObject> dimensionEmbeddingObjectsRepository,
            IRepository<DisplayedPoint> displayedPointsRepository,
            IDistributedCache distributedCache)
        {
            _clustersRepository = clustersRepository;
            _tilesService = tilesService;
            _tilesLevelRepository = tilesLevelRepository;
            _algorithmsRepository = algorithmsRepository;
            _displayedPointsRepository = displayedPointsRepository;
            _mapper = mapper;
            _localizer = localizer;
            _profilesRepository = profilesRepository;
            _embeddingObjectsGroupsRepository = embeddingObjectsGroupsRepository;
            _dimensionEmbeddingObjectsRepository = dimensionEmbeddingObjectsRepository;
            _distributedCache = distributedCache;
        }
        protected virtual async Task RemoveClusters(ClusterizationProfile profile)
        {
            for (int i = 0; i < profile.Clusters.Count; i++)
            {
                var id = profile.Clusters.ElementAt(i).Id;
                var clusterForDelete = (await _clustersRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(Cluster.DataObjects)},{nameof(Cluster.DisplayedPoints)},{nameof(Cluster.Profile)}")).FirstOrDefault();

                for(int j = 0; j < clusterForDelete.DisplayedPoints.Count; j++)
                {
                    _displayedPointsRepository.Remove(clusterForDelete.DisplayedPoints.ElementAt(j));
                }
                _clustersRepository.Remove(clusterForDelete);
            }
            for (int i = 0; i < profile.TilesLevels.Count; i++)
            {
                var id = profile.TilesLevels.ElementAt(i).Id;
                await _tilesService.FullRemoveTilesLevel(id);
            }

            profile.Clusters.Clear();
            profile.TilesLevels.Clear();

            await _profilesRepository.SaveChangesAsync();
        }
        protected virtual async Task AddTiles(ClusterizationProfile profile, List<TileGeneratingHelpModel> helpModels)
        {
            var tilesLevel = new ClusterizationTilesLevel()
            {
                Profile = profile,
                TileCount = TILES_COUNT,
                Z = 0
            };
            var tiles = await _tilesService.GenerateOneLevelTiles(helpModels, TILES_COUNT, 0, tilesLevel, profile.DRTechniqueId, profile.EmbeddingModelId, profile.WorkspaceId);
            tilesLevel.Tiles = tiles;
            await _tilesLevelRepository.AddAsync(tilesLevel);
            profile.MaxTileLevel = 0;
            profile.MinTileLevel = 0;

            profile.Tiles = tiles;
            profile.IsCalculated = true;
        }
        protected async Task WorkspaceVerification(int profileId)
        {
            var profile = (await _profilesRepository.GetAsync(e => e.Id == profileId, includeProperties: $"{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();
            if (profile == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], HttpStatusCode.NotFound);

            if (profile.IsInCalculation)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.ProfileIsInCalculation], HttpStatusCode.BadRequest);
            }
        }
        protected async Task<List<AddEmbeddingsWithDRHelpModel>> CreateHelpModels(ICollection<MyDataObject> dataObjects, string DRTechniqueId, string embeddingModelId, int workspaceId, int dimensionCount)
        {
            List<AddEmbeddingsWithDRHelpModel> helpModels = new List<AddEmbeddingsWithDRHelpModel>();

            for (int i = 0; i < dataObjects.Count(); i++)
            {
                var dataObject = dataObjects.ElementAt(i);

                var embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DRTechniqueId == DRTechniqueId && e.EmbeddingModelId == embeddingModelId && e.WorkspaceId == workspaceId && e.DataObjectId == dataObject.Id, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();

                if (embeddingObjectsGroup == null)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);
                }
                var dimensionEmbedding = (await _dimensionEmbeddingObjectsRepository.GetAsync(e => e.EmbeddingObjectsGroupId == embeddingObjectsGroup.Id && e.TypeId == dimensionCount)).FirstOrDefault();

                if (dimensionEmbedding == null)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);
                }
                var embedding = dimensionEmbedding.ValuesString.Split(' ').Select(double.Parse).ToArray();

                helpModels.Add(new AddEmbeddingsWithDRHelpModel()
                {
                    DataObject = dataObject,
                    DataPoints = embedding
                });
            }

            return helpModels;
        }


        public async Task<ICollection<AlgorithmDTO>> GetAllAlgorithms()
        {
            var algorithms = await _algorithmsRepository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}");

            return _mapper.Map<ICollection<AlgorithmDTO>>(algorithms);
        }

        public async Task<bool> ReviewWorkspaceIsProfilesInCalculation(int workspaceId)
        {
            return (await _profilesRepository.GetAsync(e => e.WorkspaceId == workspaceId && e.IsInCalculation)).Any();
        }

        public async Task RemoveCache(int profileId)
        {
            await _distributedCache.RemoveAsync("clusters" + profileId);
        }
    }
}
