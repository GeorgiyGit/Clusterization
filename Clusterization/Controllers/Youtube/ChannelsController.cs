using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Interfaces.Youtube;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Youtube
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly IYoutubeChannelsService service;
        public ChannelsController(IYoutubeChannelsService service)
        {
            this.service = service;
        }

        [HttpPost("load_by_id/{id}")]
        public async Task<IActionResult> LoadChannelById([FromRoute] string id)
        {
            await service.LoadById(id);
            return Ok();
        }
        [HttpPost("load_many_by_ids")]
        public async Task<IActionResult> LoadMultipleChannels([FromBody] LoadManyByIdsRequest request)
        {
            await service.LoadManyByIds(request.Ids);
            return Ok();
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> GetLoadedChannelById([FromRoute] string id)
        {
            return Ok(await service.GetLoadedById(id));
        }

        [HttpPost("get_many")]
        public async Task<IActionResult> GetLoadedChannels([FromBody] GetChannelsRequest request)
        {
            return Ok(await service.GetLoadedCollection(request));
        }

        [HttpPost("get_without_loading")]
        public async Task<IActionResult> GetWithoutLoading([FromBody] GetWithoutLoadingRequest request)
        {
            return Ok(await service.GetCollectionWithoutLoadingByName(request.Name, request.NextPageToken,request.FilterType));
        }
    }
}
