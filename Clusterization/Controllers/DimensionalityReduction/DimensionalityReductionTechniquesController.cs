using Domain.Interfaces.Clusterization;
using Domain.Interfaces.DimensionalityReduction;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DimensionalityReduction
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimensionalityReductionTechniquesController : ControllerBase
    {
        private readonly IDimensionalityReductionTechniquesService service;
        public DimensionalityReductionTechniquesController(IDimensionalityReductionTechniquesService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllTechniques()
        {
            return Ok(await service.GetAll());
        }
    }
}
