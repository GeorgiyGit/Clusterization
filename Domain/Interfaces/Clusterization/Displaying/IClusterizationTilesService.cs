using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Entities.Clusterization.Displaying;
using Domain.HelpModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Displaying
{
    public interface IClusterizationTilesService
    {
        public Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<TileGeneratingHelpModel> entityHelpModels, int tilesCount, int z, ClusterizationTilesLevel tilesLevel, string DRTechniqueId, string embeddingModelId, int workspaceId);

        public Task<ClusterizationTileDTO> GetOneTile(int profileId, int x, int y, int z, ICollection<int> allowedClusterIds);
        public Task<ClusterizationTileDTO> GetOneTile(int tileId, ICollection<int> allowedClusterIds);

        public Task<ICollection<ClusterizationTileDTO>> GetTileCollection(int profileId, int z, ICollection<MyIntegerVector2> points, ICollection<int> allowedClusterIds);

        public Task<ClusterizationTilesLevelDTO> GetTilesLevel(int profileId, int z);

        public Task FullRemoveTilesLevel(int tilesLevelId);
    }
}
