using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.Interfaces.Quotas;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Quotas
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotasPacksController:ControllerBase
    {
        private readonly IQuotasPacksService service;
        public QuotasPacksController(IQuotasPacksService service)
        {
            this.service = service;
        }

        [HttpPost("add_pack")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> Add([FromBody] AddQuotasPackDTO model)
        {
            await service.Add(model);
            return Ok();
        }

        [HttpGet("get_all_packs")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAll());
        }
    }
}
