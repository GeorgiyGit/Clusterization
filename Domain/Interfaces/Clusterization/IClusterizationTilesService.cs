using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationTilesService
    {
        public Task<ICollection<ClusterizationTile>> GenerateOneLevelTiles(ICollection<ClusterizationEntity> entities, int tilesCount, int z);
    }
}
