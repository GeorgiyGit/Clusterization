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
    public class QuotasLogsController:ControllerBase
    {
        private readonly IQuotasLogsService service;
        public QuotasLogsController(IQuotasLogsService service)
        {
            this.service = service;
        }

        [HttpPost("get_quotas_logs")]
        [Authorize]
        public async Task<IActionResult> GetQuotasLogs([FromBody] GetQuotasLogsRequest request)
        {
            return Ok(await service.GetQuotasLogs(request));
        }

        [HttpPost("get_quotas_pack_logs")]
        [Authorize]
        public async Task<IActionResult> GetQuotasPackLogs([FromBody] GetQuotasPackLogsRequest request)
        {
            return Ok(await service.GetQuotasPackLogs(request));
        }
    }
}
