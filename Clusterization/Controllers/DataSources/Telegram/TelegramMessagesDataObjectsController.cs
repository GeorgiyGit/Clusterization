using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using Domain.Interfaces.DataSources.Telegram;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Telegram
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramMessagesDataObjectsController : ControllerBase
    {
        private readonly ITelegramMessagesDataObjectsService service;
        public TelegramMessagesDataObjectsController(ITelegramMessagesDataObjectsService service)
        {
            this.service = service;
        }

        [HttpPost("add_messages_by_channel")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddMessagesByChannel([FromBody] AddTelegramMessagesToWorkspaceByChannelRequest request)
        {
            await service.LoadMessagesByChannel(request);
            return Ok();
        }
    }
}
