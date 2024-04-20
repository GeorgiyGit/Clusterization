using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.EmbeddingModels;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.EmbeddingModels
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbeddingModelsController : ControllerBase
    {
        private readonly IEmbeddingModelsService service;
        public EmbeddingModelsController(IEmbeddingModelsService service)
        {
            this.service = service; 
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllEmbeddingModels()
        {
            return Ok(await service.GetAll());
        }
    }
}
