using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.DataObjects;
using Domain.Entities.Embeddings;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.HelpModels;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;

namespace Domain.Services.Clusterization.Displaying
{
    public class ClusterizationTilesService : IClusterizationTilesService
    {
        private readonly IRepository<DisplayedPoint> _displayedPointsRepository;
        private readonly IRepository<ClusterizationTile> _tilesRepository;
        private readonly IRepository<ClusterizationTilesLevel> _tilesLevelRepository;
        private readonly IRepository<EmbeddingObjectsGroup> _embeddingObjectsGroupsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IMapper _mapper;
        public ClusterizationTilesService(IRepository<DisplayedPoint> displayedPoints_repository,
                                          IRepository<ClusterizationTile> tiles_repository,
                                          IStringLocalizer<ErrorMessages> localizer,
                                          IMapper mapper,
                                          IRepository<ClusterizationTilesLevel> tilesLevel_repository,
                                          IRepository<EmbeddingObjectsGroup> embeddingObjectsGroupsRepository)
        {
            _displayedPointsRepository = displayedPoints_repository;
            _tilesRepository = tiles_repository;
            _localizer = localizer;
            _tilesLevelRepository = tilesLevel_repository;
            _embeddingObjectsGroupsRepository = embeddingObjectsGroupsRepository;
            
            _mapper = mapper;
        }
        public async Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<TileGeneratingHelpModel> entityHelpModels, int tilesCount, int z, ClusterizationTilesLevel tilesLevel, string DRTechniqueId, string embeddingModelId, int workspaceId)
        {
            foreach (var helpModel in entityHelpModels)
            {
                var embeddingObjectsGroup = (await _embeddingObjectsGroupsRepository.GetAsync(e => e.DRTechniqueId == DRTechniqueId && e.EmbeddingModelId == embeddingModelId && e.WorkspaceId == workspaceId && e.DataObjectId == helpModel.DataObject.Id, includeProperties: $"{nameof(EmbeddingObjectsGroup.DimensionEmbeddingObjects)}")).FirstOrDefault();
                if (embeddingObjectsGroup == null) throw new HttpException(_localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);

                var dimensionEmbedding = embeddingObjectsGroup.DimensionEmbeddingObjects.Where(e => e.TypeId == 2).FirstOrDefault();
                if (dimensionEmbedding == null) throw new HttpException(_localizer[ErrorMessagePatterns.NotAllDataEmbedded], HttpStatusCode.BadRequest);


                helpModel.EmbeddingValues = dimensionEmbedding.ValuesString.Split(' ').Select(double.Parse).ToArray();
            }

            var minXValue = entityHelpModels.Min(e => e.EmbeddingValues[0]);
            var maxXValue = entityHelpModels.Max(e => e.EmbeddingValues[0]);

            var minYValue = entityHelpModels.Min(e => e.EmbeddingValues[1]);
            var maxYValue = entityHelpModels.Max(e => e.EmbeddingValues[1]);

            tilesLevel.MinXValue = minXValue;
            tilesLevel.MinYValue = minYValue;

            tilesLevel.MaxXValue = maxXValue;
            tilesLevel.MaxYValue = maxYValue;

            double maxLength = 0d;

            if (maxXValue - minXValue > maxYValue - minYValue)
            {
                maxLength = maxXValue - minXValue;
            }
            else
            {
                maxLength = maxYValue - minYValue;
            }

            double lengthPerTile = maxLength / tilesCount;

            tilesLevel.TileLength = lengthPerTile;

            var tiles = new List<ClusterizationTile>(tilesCount * tilesCount);

            for (int y = 0; y < tilesCount; y++)
            {
                for (int x = 0; x < tilesCount; x++)
                {
                    var helpModelsInTheTile = entityHelpModels.Where(e => e.EmbeddingValues[0] >= lengthPerTile * x + minXValue && e.EmbeddingValues[0] <= lengthPerTile * (x + 1) + minXValue &&
                                                                          e.EmbeddingValues[1] >= lengthPerTile * y + minYValue && e.EmbeddingValues[1] <= lengthPerTile * (y + 1) + minYValue).ToList();
                    var newTile = new ClusterizationTile()
                    {
                        Z = z,
                        Y = y,
                        X = x,
                        Length = lengthPerTile
                    };

                    foreach (var model in helpModelsInTheTile)
                    {
                        var point = new DisplayedPoint()
                        {
                            OptimizationLevel = z,
                            X = model.EmbeddingValues[0],
                            Y = model.EmbeddingValues[1],
                            Tile = newTile,
                            DataObjectId = model.DataObject.Id,
                            Cluster = model.Cluster
                        };

                        await _displayedPointsRepository.AddAsync(point);
                        newTile.Points.Add(point);
                    }

                    await _tilesRepository.AddAsync(newTile);

                    tiles.Add(newTile);
                }
            }

            return tiles;
        }


        #region get_tiles
        public async Task<ClusterizationTileDTO> GetOneTile(int profileId, int x, int y, int z, ICollection<int> allowedClusterIds)
        {
            var tile = (await _tilesRepository.GetAsync(c => c.ProfileId == profileId && c.X == x && c.Y == y && c.Z == z)).FirstOrDefault();

            if (tile == null) throw new HttpException(_localizer[ErrorMessagePatterns.TileNotFound], HttpStatusCode.NotFound);

            return await GetOneTile(tile.Id, allowedClusterIds);
        }
        public async Task<ClusterizationTileDTO> GetOneTile(int tileId, ICollection<int> allowedClusterIds)
        {
            var tile = (await _tilesRepository.GetAsync(c => c.Id == tileId)).FirstOrDefault();

            if (tile == null) throw new HttpException(_localizer[ErrorMessagePatterns.TileNotFound], HttpStatusCode.NotFound);

            ICollection<DisplayedPoint> points = new List<DisplayedPoint>();
            
            if (allowedClusterIds.Count() > 0)
            {
                var filterCondition = ExpressionExtensions.PointsCreateAndExpression(tileId, allowedClusterIds.ToArray());

                points = (await _displayedPointsRepository.GetAsync(filter:filterCondition, includeProperties: $"{nameof(DisplayedPoint.Cluster)}")).ToList();
            }
            else
            {
                points = (await _displayedPointsRepository.GetAsync(e => e.TileId == tileId, includeProperties: $"{nameof(DisplayedPoint.Cluster)}")).ToList();
            }
            var mappedTile = _mapper.Map<ClusterizationTileDTO>(tile);

            mappedTile.Points = _mapper.Map<ICollection<DisplayedPointDTO>>(points);

            return mappedTile;
        }

        public async Task<ICollection<ClusterizationTileDTO>> GetTileCollection(int profileId, int z, ICollection<MyIntegerVector2> points, ICollection<int> allowedClusterIds)
        {
            var tilesLevel = (await _tilesLevelRepository.GetAsync(e => e.ProfileId == profileId && e.Z == z, includeProperties: $"{nameof(ClusterizationTilesLevel.Tiles)}")).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(_localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            var tiles = new List<ClusterizationTileDTO>(points.Count());
            foreach (var point in points)
            {
                var tileId = tilesLevel.Tiles.Where(e => e.X == point.X && e.Y == point.Y).FirstOrDefault()?.Id;

                if (tileId != null)
                {
                    var tile = await GetOneTile((int)tileId,allowedClusterIds);
                    tiles.Add(tile);
                }
            }

            return tiles;
        }
        #endregion

        public async Task<ClusterizationTilesLevelDTO> GetTilesLevel(int profileId, int z)
        {
            var tilesLevel = (await _tilesLevelRepository.GetAsync(e => e.ProfileId == profileId && e.Z == z)).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(_localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<ClusterizationTilesLevelDTO>(tilesLevel);
        }

        public async Task FullRemoveTilesLevel(int tilesLevelId)
        {
            var tilesLevel = (await _tilesLevelRepository.GetAsync(e => e.Id == tilesLevelId, includeProperties: $"{nameof(ClusterizationTilesLevel.Tiles)}")).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(_localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            foreach (var tile in tilesLevel.Tiles)
            {
                var fullTile = (await _tilesRepository.GetAsync(e => e.Id == tile.Id, includeProperties: $"{nameof(ClusterizationTile.Parent)},{nameof(ClusterizationTile.ChildTiles)},{nameof(ClusterizationTile.Points)},{nameof(ClusterizationTile.TilesLevel)},{nameof(ClusterizationTile.Profile)}")).FirstOrDefault();

                if (fullTile != null)
                {
                    var points = await _displayedPointsRepository.GetAsync(e => e.TileId == fullTile.Id, includeProperties: $"{nameof(DisplayedPoint.Tile)},{nameof(DisplayedPoint.Cluster)}");

                    foreach (var point in points)
                    {
                        _displayedPointsRepository.Remove(point);
                    }
                    _tilesRepository.Remove(fullTile);
                }
            }
            _tilesLevelRepository.Remove(tilesLevel);
            await _tilesLevelRepository.SaveChangesAsync();
        }
    }
}
