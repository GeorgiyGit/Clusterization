﻿using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using Domain.Entities.Embeddings;
using Domain.Exceptions;
using Domain.HelpModels;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization
{
    public class ClusterizationTilesService : IClusterizationTilesService
    {
        private readonly IRepository<DisplayedPoint> displayedPoints_repository;
        private readonly IRepository<ClusterizationTile> tiles_repository;
        private readonly IRepository<ClusterizationTilesLevel> tilesLevel_repository;
        private readonly IRepository<DimensionalityReductionValue> drValues_repository;

        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        public ClusterizationTilesService(IRepository<DisplayedPoint> displayedPoints_repository,
                                          IRepository<ClusterizationTile> tiles_repository,
                                          IStringLocalizer<ErrorMessages> localizer,
                                          IMapper mapper,
                                          IRepository<ClusterizationTilesLevel> tilesLevel_repository,
                                          IRepository<DimensionalityReductionValue> drValues_repository)
        {
            this.displayedPoints_repository = displayedPoints_repository;
            this.tiles_repository = tiles_repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.tilesLevel_repository = tilesLevel_repository;
            this.drValues_repository = drValues_repository;
        }
        public async Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<TileGeneratingHelpModel> entityHelpModels, int tilesCount, int z, ClusterizationTilesLevel tilesLevel,string drTechniqueId)
        {
            foreach (var helpModel in entityHelpModels)
            {
                DimensionalityReductionValue drValue;
                if (drTechniqueId == DimensionalityReductionTechniques.JSL)
                {
                    drValue = (await drValues_repository.GetAsync(e => e.TechniqueId == DimensionalityReductionTechniques.JSL && e.EmbeddingDataId == helpModel.Entity.EmbeddingDataId, includeProperties: $"{nameof(DimensionalityReductionValue.Embeddings)}")).FirstOrDefault();
                }
                else
                {
                    drValue = (await drValues_repository.GetAsync(e => e.TechniqueId == drTechniqueId && e.ClusterizationEntityId == helpModel.Entity.Id, includeProperties: $"{nameof(DimensionalityReductionValue.Embeddings)}")).FirstOrDefault();
                }

                if (drValue == null) throw new HttpException(localizer[ErrorMessagePatterns.DRValueNotFound], HttpStatusCode.NotFound);

                var dimensionValue = drValue.Embeddings.First(e => e.DimensionTypeId == 2);

                double[] embeddingValues = dimensionValue.ValuesString.Split(' ').Select(double.Parse).ToArray(); 
                helpModel.EmbeddingValues = embeddingValues;
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
                    var helpModelsInTheTile = entityHelpModels.Where(e => (e.EmbeddingValues[0] >= (lengthPerTile * x+minXValue) && e.EmbeddingValues[0] <= (lengthPerTile * (x + 1) + minXValue)) &&
                                                                          (e.EmbeddingValues[1] >= (lengthPerTile * y+minYValue) && e.EmbeddingValues[1] <= (lengthPerTile * (y + 1))+minYValue)).ToList();
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
                            Value = model.Entity.TextValue, //temp,
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


        #region get_tiles
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

        public async Task<ICollection<ClusterizationTileDTO>> GetTileCollection(int profileId, int z, ICollection<MyIntegerVector2> points)
        {
            var tilesLevel = (await tilesLevel_repository.GetAsync(e => e.ProfileId == profileId && e.Z == z, includeProperties: $"{nameof(ClusterizationTilesLevel.Tiles)}")).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            var tiles = new List<ClusterizationTileDTO>(points.Count());
            foreach(var point in points)
            {
                var tileId = tilesLevel.Tiles.Where(e => e.X == point.X && e.Y == point.Y).FirstOrDefault()?.Id;

                if (tileId != null)
                {
                    var tile = await GetOneTile((int)tileId);
                    tiles.Add(tile);
                }
            }

            return tiles;
        }
        #endregion

        public async Task<ClusterizationTilesLevelDTO> GetTilesLevel(int profileId, int z)
        {
            var tilesLevel = (await tilesLevel_repository.GetAsync(e => e.ProfileId == profileId && e.Z == z)).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            return mapper.Map<ClusterizationTilesLevelDTO>(tilesLevel);
        }

        public async Task FullRemoveTilesLevel(int tilesLevelId)
        {
            var tilesLevel = (await tilesLevel_repository.GetAsync(e => e.Id==tilesLevelId,includeProperties:$"{nameof(ClusterizationTilesLevel.Tiles)}")).FirstOrDefault();

            if (tilesLevel == null) throw new HttpException(localizer[ErrorMessagePatterns.TilesLevelNotFound], HttpStatusCode.NotFound);

            foreach(var tile in tilesLevel.Tiles)
            {
                var fullTile = (await tiles_repository.GetAsync(e => e.Id == tile.Id, includeProperties: $"{nameof(ClusterizationTile.Parent)},{nameof(ClusterizationTile.ChildTiles)},{nameof(ClusterizationTile.Points)},{nameof(ClusterizationTile.TilesLevel)},{nameof(ClusterizationTile.Profile)}")).FirstOrDefault();

                if (fullTile != null)
                {
                    var points = await displayedPoints_repository.GetAsync(e => e.TileId == fullTile.Id, includeProperties: $"{nameof(DisplayedPoint.Tile)},{nameof(DisplayedPoint.Cluster)}");

                    foreach(var point in points)
                    {
                        displayedPoints_repository.Remove(point);
                    }
                    tiles_repository.Remove(fullTile);
                }
            }
            tilesLevel_repository.Remove(tilesLevel);
            await tilesLevel_repository.SaveChangesAsync();
        }
    }
}
