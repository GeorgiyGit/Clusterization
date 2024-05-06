using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Youtube
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeVideosController : ControllerBase
    {
        private readonly IYoutubeVideoService service;

        public YoutubeVideosController(IYoutubeVideoService service)
        {
            this.service = service;
        }

        [HttpPost("load_by_id/{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadById([FromRoute] string id)
        {
            await service.LoadById(id);
            return Ok();
        }

        [HttpPost("load_from_channel")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadFromChannel([FromBody] YoutubeLoadOptions options)
        {
            await service.LoadFromChannel(options);
            return Ok();
        }

        [HttpPost("load_many_by_ids")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadManyByIds([FromBody] LoadManyByIdsRequest request)
        {
            await service.LoadManyByIds(request.Ids);
            return Ok();
        }

        [HttpGet("get_loaded_by_id/{id}")]
        public async Task<IActionResult> GetLoadedById([FromRoute] string id)
        {
            return Ok(await service.GetLoadedById(id));
        }

        [HttpPost("get_loaded_collection")]
        public async Task<IActionResult> GetLoadedCollection([FromBody] GetYoutubeVideosRequest request)
        {
            return Ok(await service.GetLoadedCollection(request));
        }

        [HttpPost("get_without_loading")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetWithoutLoading([FromBody] GetWithoutLoadingRequest request)
        {
            return Ok(await service.GetCollectionWithoutLoadingByName(request.Name, request.NextPageToken, request.ChannelId, request.FilterType));
        }
    }
}
