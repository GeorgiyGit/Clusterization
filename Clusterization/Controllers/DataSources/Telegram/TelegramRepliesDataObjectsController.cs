using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using Domain.Interfaces.DataSources.Telegram;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Telegram
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramRepliesDataObjectsController : ControllerBase
    {
        private readonly ITelegramRepliesDataObjectsService service;
        public TelegramRepliesDataObjectsController(ITelegramRepliesDataObjectsService service)
        {
            this.service = service;
        }

        [HttpPost("add_replies_by_channel")]
        [Authorize]
        public async Task<IActionResult> LoadReplisByChannel([FromBody] AddTelegramRepliesToWorkspaceByChannelRequest request)
        {
            await this.service.LoadRepliesByChannel(request);
            return Ok();
        }

        [HttpPost("add_replies_by_messages")]
        [Authorize]
        public async Task<IActionResult> LoadRepliesByMessages([FromBody] AddTelegramRepliesToWorkspaceByMessagesRequest request)
        {
            await this.service.LoadRepliesByMessages(request);
            return Ok();
        }
    }
}
