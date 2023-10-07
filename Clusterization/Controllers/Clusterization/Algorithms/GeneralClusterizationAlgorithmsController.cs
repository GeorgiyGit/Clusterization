using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralClusterizationAlgorithmsController : ControllerBase
    {
        private readonly IGeneralClusterizationAlgorithmService service;
        public GeneralClusterizationAlgorithmsController(IGeneralClusterizationAlgorithmService service)
        {
            this.service = service;
        }

        [HttpGet("get_all/{typeId}")]
        public async Task<IActionResult> GetAllAlgorithmsByTypeId(string typeId)
        {
            return Ok(await service.GetAllAlgorithms(typeId));
        }
    }
}
