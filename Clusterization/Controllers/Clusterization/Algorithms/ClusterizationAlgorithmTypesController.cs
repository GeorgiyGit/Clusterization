using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Services.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationAlgorithmTypesController : ControllerBase
    {
        private readonly IClusterizationAlgorithmTypesService service;
        public ClusterizationAlgorithmTypesController(IClusterizationAlgorithmTypesService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllAlgorithmTypes()
        {
            return Ok(await service.GetAllTypes());
        }
    }
}
