using Domain.Interfaces.Clusterization.Dimensions;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Dimensions
{

    [Route("api/[controller]")]
    [ApiController]
    public class DimensionTypesController : ControllerBase
    {
        private readonly IClusterizationDimensionTypesService service;
        public DimensionTypesController(IClusterizationDimensionTypesService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllDimensionTypes()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("get_all_in_embedding_model/{id}")]
        public async Task<IActionResult> GetAllInEmbeddingModel([FromRoute] string id)
        {
            return Ok(await service.GetAllInEmbeddingModel(id));
        }
    }
}
