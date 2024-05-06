using Domain.DTOs.CustomerDTOs.Requests;
using Domain.Interfaces.Customers;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("send_email_confirmation")]
        [Authorize]
        public async Task<IActionResult> SendEmailConfirmation()
        {
            await service.SendEmailConfirmation();
            return Ok();
        }

        [HttpPost("confirm_email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            return Ok(await service.ConfirmEmail(request.Token, request.Email));
        }

        [HttpGet("check_email_confirmation")]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail()
        {
            return Ok(await service.CheckEmailConfirmation());
        }
    }
}
