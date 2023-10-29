﻿using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.Entities.Clusterization;
using Domain.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationTilesService
    {
        public Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<TileGeneratingHelpModel> entityHelpModels, int tilesCount, int z);

        public Task<ICollection<DisplayedPointDTO>> GetOneTilePoints(int profileId, int x, int y, int z);
        public Task<ICollection<DisplayedPointDTO>> GetOneTilePoints(int tileId);
    }
}
