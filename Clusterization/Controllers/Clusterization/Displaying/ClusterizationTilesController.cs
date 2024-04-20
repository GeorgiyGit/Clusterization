using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.DTOs.ClusterizationDTOs.TileDTOs;
using Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs;
using Domain.Interfaces.Clusterization.Displaying;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Displaying
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationTilesController : ControllerBase
    {
        private readonly IClusterizationTilesService service;
        public ClusterizationTilesController(IClusterizationTilesService service)
        {
            this.service = service;
        }

        [HttpPost("get_tile_by_profile")]
        public async Task<IActionResult> GetOneTileByProfile([FromBody] GetTileByProfileIdRequest request)
        {
            return Ok(await service.GetOneTile(request.ProfileId, request.X, request.Y, request.Z));
        }

        [HttpGet("get_tile_by_id/{id}")]
        public async Task<IActionResult> GetOneTileById([FromRoute] int id)
        {
            return Ok(await service.GetOneTile(id));
        }

        [HttpPost("get_tile_collection")]
        public async Task<IActionResult> GetTileCollection([FromBody] GetTileCollectionRequest request)
        {
            return Ok(await service.GetTileCollection(request.ProfileId, request.Z, request.Points));
        }

        [HttpPost("get_tiles_level_by_profile")]
        public async Task<IActionResult> GetTilesLevelByProfile([FromBody] GetTilesLevelByProfileIdRequest request)
        {
            return Ok(await service.GetTilesLevel(request.ProfileId, request.Z));
        }
    }
}
