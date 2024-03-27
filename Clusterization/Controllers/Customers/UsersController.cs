using Domain.DTOs.CustomerDTOs.Requests;
using Domain.Interfaces.Customers;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;
        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("get_customers")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> GetCustomers([FromBody] GetCustomersRequest request)
        {
            return Ok(await service.GetCustomers(request));
        }
    }
}
