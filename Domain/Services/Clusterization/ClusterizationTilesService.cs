using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization
{
    public class ClusterizationTilesService : IClusterizationTilesService
    {
        private readonly IRepository<EmbeddingData> embeddingData_repository;
        private readonly IRepository<EmbeddingValue> embeddingValue_repository;
        private readonly IRepository<DisplayedPoint> displayedPoints_repository;
        private readonly IRepository<ClusterizationTile> tiles_repository;
        private readonly IRepository<ClusterizationTilesLevel> tilesLevel_repository;

        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        public ClusterizationTilesService(IRepository<EmbeddingData> embeddingData_repository,
                                          IRepository<EmbeddingValue> embeddingValue_repository,
                                          IRepository<DisplayedPoint> displayedPoints_repository,
                                          IRepository<ClusterizationTile> tiles_repository,
                                          IStringLocalizer<ErrorMessages> localizer,
                                          IMapper mapper,
                                          IRepository<ClusterizationTilesLevel> tilesLevel_repository)
        {
            this.embeddingData_repository = embeddingData_repository;
            this.embeddingValue_repository = embeddingValue_repository;
            this.displayedPoints_repository = displayedPoints_repository;
            this.tiles_repository = tiles_repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.tilesLevel_repository = tilesLevel_repository;
        }
        public async Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<TileGeneratingHelpModel> entityHelpModels, int tilesCount, int z)
        {          
            foreach(var helpModel in entityHelpModels)
            {
                var embeddingData = (await embeddingData_repository.GetAsync(c => c.Id == helpModel.Entity.EmbeddingDataId, includeProperties: $"{nameof(EmbeddingData.Embeddings)}")).First();

                var dimensionValue = embeddingData.Embeddings.First(e => e.DimensionTypeId == 2);

                var embeddingValues = (await embeddingValue_repository.GetAsync(e => e.EmbeddingDimensionValueId == dimensionValue.Id,orderBy:e=>e.OrderBy(e=>e.Id))).ToList();

                helpModel.EmbeddingValues = embeddingValues;
            }

            var minXValue = entityHelpModels.Min(e => e.EmbeddingValues[0].Value);
            var maxXValue = entityHelpModels.Max(e => e.EmbeddingValues[0].Value);

            var minYValue = entityHelpModels.Min(e => e.EmbeddingValues[1].Value);
            var maxYValue = entityHelpModels.Max(e => e.EmbeddingValues[1].Value);

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

            var tiles = new List<ClusterizationTile>(tilesCount * tilesCount);

            for (int y = 0; y < tilesCount; y++)
            {
                for (int x = 0; x < tilesCount; x++)
                {
                    var helpModelsInTheTile = entityHelpModels.Where(e => (e.EmbeddingValues[0].Value >= (lengthPerTile * x+minXValue) && e.EmbeddingValues[0].Value <= (lengthPerTile * (x + 1) + minXValue)) &&
                                                                          (e.EmbeddingValues[1].Value >= (lengthPerTile * y+minYValue) && e.EmbeddingValues[1].Value <= (lengthPerTile * (y + 1))+minYValue)).ToList();
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
                            X = model.EmbeddingValues[0].Value,
                            Y = model.EmbeddingValues[1].Value,
                            Tile = newTile,
                            ValueId = model.Entity.CommentId, //temp,
                            Cluster = model.Cluster
                        };

                        await displayedPoints_repository.AddAsync(point);
                        newTile.Points.Add(point);
                    }

                    await tiles_repository.AddAsync(newTile);

                    tiles.Add(newTile);
                }
            }

            return tiles;
        }


        public async Task<ClusterizationTileDTO> GetOneTile(int profileId, int x, int y, int z)
        {
            var tile = (await tiles_repository.GetAsync(c => c.ProfileId == profileId && c.X == x && c.Y == y && c.Z == z)).FirstOrDefault();

            if (tile == null) throw new HttpException(localizer[ErrorMessagePatterns.TileNotFound], HttpStatusCode.NotFound);

            return await GetOneTile(tile.Id);
        }
        public async Task<ClusterizationTileDTO> GetOneTile(int tileId)
        {
            var tile = (await tiles_repository.GetAsync(c => c.Id == tileId)).FirstOrDefault();

            if (tile == null) throw new HttpException(localizer[ErrorMessagePatterns.TileNotFound], HttpStatusCode.NotFound);

            var points = (await displayedPoints_repository.GetAsync(e => e.TileId == tileId,includeProperties:$"{nameof(DisplayedPoint.Cluster)}")).ToList();

            var mappedTile = mapper.Map<ClusterizationTileDTO>(tile);

            mappedTile.Points = mapper.Map<ICollection<DisplayedPointDTO>>(points);

            return mappedTile;
        }


        public async Task<ClusterizationTilesLevelDTO> GetTilesLevel(int profileId, int x)
        {
            var tilesLevel = (await tilesLevel_repository.GetAsync(e => e.ProfileId == profileId && e.X == x)).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            return mapper.Map<ClusterizationTilesLevelDTO>(tilesLevel);
        }
    }
}
