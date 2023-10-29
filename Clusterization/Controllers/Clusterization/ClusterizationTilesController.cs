using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Interfaces.Clusterization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
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

        [HttpPost("get_tile_displayed_points_by_profileid")]
        public async Task<IActionResult> GetTileDisplayedPointsByProfileId([FromBody] GetDisplayedPointsByProfileIdRequest request)
        {
            return Ok(await service.GetOneTilePoints(request.ProfileId, request.X, request.Y, request.Z));
        }

        [HttpGet("get_displayed_points_by_tileId/{id}")]
        public async Task<IActionResult> GetDisplayedPointsByTileId([FromRoute] int id)
        {
            return Ok(await service.GetOneTilePoints(id));
        }
    }
}
