using Domain.Interfaces.Clusterization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationTypesController:ControllerBase
    {
        private readonly IClusterizationTypeService service;
        public ClusterizationTypesController(IClusterizationTypeService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllTypes()
        {
            return Ok(await service.GetAll());
        }
    }
}
