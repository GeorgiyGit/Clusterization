using Domain.DTOs.CustomerDTOs.Requests;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Youtube;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp([FromBody] CustomerSignUpRequest request)
        {
            return Ok(await service.SignUp(request));
        }

        [HttpPost("log_in")]
        public async Task<IActionResult> LogIn([FromBody] CustomerLogInRequest request)
        {
            return Ok(await service.LogIn(request));
        }
    }
}
