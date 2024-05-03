using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Interfaces.DataSources.Telegram;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Telegram
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramChannelsController : ControllerBase
    {
        private readonly ITelegramChannelsService service;
        public TelegramChannelsController(ITelegramChannelsService service)
        {
            this.service = service;
        }

        [HttpPost("load_by_username/{username}")]
        [Authorize]
        public async Task<IActionResult> LoadByUsername([FromRoute] string username)
        {
            await service.LoadByUsername(username);
            return Ok();
        }
        [HttpPost("load_many_by_usernames")]
        [Authorize]
        public async Task<IActionResult> LoadManyByUsernames([FromBody] LoadManyByIdsRequest request)
        {
            await service.LoadManyByUsernames(request.Ids);
            return Ok();
        }

        [HttpGet]
        [Route("get_loaded_by_id/{id}")]
        public async Task<IActionResult> GetLoadedById([FromRoute] long id)
        {
            return Ok(await service.GetLoadedById(id));
        }

        [HttpPost("get_loaded_collection")]
        public async Task<IActionResult> GetLoadedCollection([FromBody] GetTelegramChannelsRequest request)
        {
            return Ok(await service.GetLoadedCollection(request));
        }

        [HttpGet("get_without_loading/{name}")]
        [Authorize]
        public async Task<IActionResult> GetWithoutLoading([FromRoute] string name)
        {
            return Ok(await service.GetCollectionWithoutLoadingByName(name));
        }
    }
}
