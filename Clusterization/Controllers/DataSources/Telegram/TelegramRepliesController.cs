using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Interfaces.DataSources.Telegram;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Telegram
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramRepliesController : ControllerBase
    {
        private readonly ITelegramRepliesService service;
        public TelegramRepliesController(ITelegramRepliesService service)
        {
            this.service = service;
        }

        [HttpPost("load_from_message")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadFromMessage([FromBody] TelegramLoadOptions options)
        {
            await service.LoadFromMessage(options);
            return Ok();
        }
        [HttpPost("load_from_channel")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadFromChannel([FromBody] LoadTelegramRepliesByChannelOptions options)
        {
            await service.LoadFromChannel(options);
            return Ok();
        }
    }
}
