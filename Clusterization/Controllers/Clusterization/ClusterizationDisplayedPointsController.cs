using Domain.Interfaces.Clusterization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationDisplayedPointsController : ControllerBase
    {
        private readonly IClusterizationDisplayedPointsService service;
        public ClusterizationDisplayedPointsController(IClusterizationDisplayedPointsService service)
        {
            this.service = service;
        }

        [HttpGet("get_point_value/{pointId}")]
        public async Task<IActionResult> GetPointValue([FromRoute] int pointId)
        {
            return Ok(await service.GetDisplayedPointTextValue(pointId));
        }
    }
}
