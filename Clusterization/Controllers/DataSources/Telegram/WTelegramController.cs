using Domain.Interfaces.DataSources.Youtube;
using Domain.Resources.Types;
using Domain.Services.DataSources.Telegram;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Telegram
{
    [Route("api/[controller]")]
    [ApiController]
    public class WTelegramController : ControllerBase
    {
        private readonly WTelegramService service;
        public WTelegramController(WTelegramService service)
        {
            this.service = service;
        }
        [HttpGet("status")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> Status()
        {
            return Ok(service.ConfigNeeded);
        }

        [HttpPost("config_verification_code")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> AddVerificationCode([FromBody] string code)
        {
            await service.DoLogin(code);
            return Ok();
        }
    }
}
