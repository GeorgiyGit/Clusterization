using Domain.Interfaces.Clusterization.Dimensions;
using Domain.Interfaces.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationDimensionTypesController:ControllerBase
    {
        private readonly IClusterizationDimensionTypesService service;
        public ClusterizationDimensionTypesController(IClusterizationDimensionTypesService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllDimensionTypes()
        {
            return Ok(await service.GetAll());
        }
    }
}
