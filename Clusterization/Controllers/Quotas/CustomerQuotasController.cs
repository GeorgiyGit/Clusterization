using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Quotas
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQuotasController: ControllerBase
    {
        private readonly ICustomerQuotasService service;
        public CustomerQuotasController(ICustomerQuotasService service)
        {
            this.service = service;
        }

        [HttpPost("add_to_customer")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> AddQuotesPackToCustomer([FromBody] AddQuotasToCustomerDTO request)
        {
            await service.AddQuotesPackToCustomer(request);
            return Ok();
        }

        [HttpGet("get_all")]
        [Authorize]
        public async Task<IActionResult> GetAllCustomerQuotes()
        {
            return Ok(await service.GetAllCustomerQuotes());
        }
    }
}
