using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using Domain.Interfaces.DataSources.Telegram;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Telegram
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramMessagesController : ControllerBase
    {
        private readonly ITelegramMessagesService service;

        public TelegramMessagesController(ITelegramMessagesService service)
        {
            this.service = service;
        }

        [HttpPost("load_by_id")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadById([FromBody] TelegramMessageLoadByIdRequest request)
        {
            await service.LoadById(request.Id, request.ChannelId);
            return Ok();
        }

        [HttpPost("load_from_channel")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadFromChannel([FromBody] TelegramLoadOptions request)
        {
            await service.LoadFromChannel(request);
            return Ok();
        }

        [HttpPost("load_many_by_ids")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadManyByIds([FromBody] TelegramMessageLoadManyByIdsRequest request)
        {
            await service.LoadManyByIds(request.Ids, request.ChannelId);
            return Ok();
        }

        [HttpGet("get_loaded_by_id/{id}")]
        public async Task<IActionResult> GetLoadedById([FromRoute] long id)
        {
            return Ok(await service.GetLoadedById(id));
        }

        [HttpPost("get_loaded_collection")]
        public async Task<IActionResult> GetLoadedCollection([FromBody] GetTelegramMessagesRequest request)
        {
            return Ok(await service.GetLoadedCollection(request));
        }
        [HttpPost("get_customer_loaded_collection")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetCustomerLoadedCollection([FromBody] GetTelegramMessagesRequest request)
        {
            return Ok(await service.GetCustomerLoadedCollection(request));
        }

        [HttpPost("get_without_loading")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetWithoutLoading([FromBody] GetTelegramMessagesRequest request)
        {
            return Ok(await service.GetCollectionWithoutLoadingByName(request));
        }
    }
}
