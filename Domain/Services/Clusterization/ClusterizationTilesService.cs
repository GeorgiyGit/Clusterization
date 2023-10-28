﻿using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using Domain.HelpModels;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ClusterizationTilesService(IRepository<EmbeddingData> embeddingData_repository,
                                          IRepository<EmbeddingValue> embeddingValue_repository,
                                          IRepository<DisplayedPoint> displayedPoints_repository,
                                          IRepository<ClusterizationTile> tiles_repository)
        {
            this.embeddingData_repository = embeddingData_repository;
            this.embeddingValue_repository = embeddingValue_repository;
            this.displayedPoints_repository = displayedPoints_repository;
            this.tiles_repository = tiles_repository;
        }
        public async Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<ClusterizationEntity> entities, int tilesCount, int z)
        {
            List<TileGeneratingHelpModel> helpModels = new List<TileGeneratingHelpModel>(entities.Count());
            foreach(var entity in entities)
            {
                var embeddingData = (await embeddingData_repository.GetAsync(c => c.Id == entity.EmbeddingDataId, includeProperties: $"{nameof(EmbeddingData.Embeddings)}")).First();

                var dimensionValue = embeddingData.Embeddings.First(e => e.DimensionTypeId == 2);

                var embeddingValues = (await embeddingValue_repository.GetAsync(e => e.EmbeddingDimensionValueId == dimensionValue.Id)).ToList();

                var helpModel = new TileGeneratingHelpModel()
                {
                    Entity = entity,
                    EmbeddingValues = embeddingValues
                };

                helpModels.Add(helpModel);
            }

            var minXValue = helpModels.Min(e => e.EmbeddingValues[0].Value);
            var maxXValue = helpModels.Max(e => e.EmbeddingValues[0].Value);

            var minYValue = helpModels.Min(e => e.EmbeddingValues[1].Value);
            var maxYValue = helpModels.Max(e => e.EmbeddingValues[1].Value);

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
                    var helpModelsInTheTile = helpModels.Where(e => (e.EmbeddingValues[0].Value >= lengthPerTile * x && e.EmbeddingValues[0].Value < lengthPerTile * (x + 1)) &&
                                                                  (e.EmbeddingValues[1].Value >= lengthPerTile * y && e.EmbeddingValues[1].Value < lengthPerTile * (y + 1))).ToList();

                    var newTile = new ClusterizationTile()
                    {
                        Z = z,
                        Y = y,
                        X = x
                    };

                    foreach (var model in helpModelsInTheTile)
                    {
                        var point = new DisplayedPoint()
                        {
                            ClusterizationEntity = model.Entity,
                            OptimizationLevel = z,
                            X = model.EmbeddingValues[0].Value,
                            Y = model.EmbeddingValues[1].Value,
                            Tile = newTile,
                            ValueId = model.Entity.CommentId //temp
                        };

                        await displayedPoints_repository.AddAsync(point);
                        newTile.Points.Add(point);
                    }

                    await tiles_repository.AddAsync(newTile);
                }
            }

            return tiles;
        }
    }
}
