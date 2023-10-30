using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Entities.Clusterization;
using Domain.HelpModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationTilesService
    {
        public Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<TileGeneratingHelpModel> entityHelpModels, int tilesCount, int z, ClusterizationTilesLevel tilesLevel);

        public Task<ClusterizationTileDTO> GetOneTile(int profileId, int x, int y, int z);
        public Task<ClusterizationTileDTO> GetOneTile(int tileId);

        public Task<ICollection<ClusterizationTileDTO>> GetTileCollection(int profileId, int z, ICollection<MyIntegerVector2> points);

        public Task<ClusterizationTilesLevelDTO> GetTilesLevel(int profileId, int z);
    }
}
