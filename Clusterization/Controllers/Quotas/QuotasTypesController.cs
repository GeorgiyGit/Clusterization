using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.Interfaces.Quotas;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Quotas
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotasTypesController:ControllerBase
    {
        private readonly IQuotasTypesService service;
        public QuotasTypesController(IQuotasTypesService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllTypes()
        {
            return Ok(await service.GetAllTypes());
        }
    }
}
