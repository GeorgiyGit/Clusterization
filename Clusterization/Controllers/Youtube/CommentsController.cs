using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Interfaces.Youtube;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Youtube
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IYoutubeCommentsService service;
        public CommentsController(IYoutubeCommentsService service)
        {
            this.service = service;
        }

        [HttpPost("load_from_video")]
        public async Task<IActionResult> LoadFromVideo([FromBody] LoadOptions options)
        {
            await service.LoadFromVideo(options);
            return Ok();
        }

        [HttpGet("get_loaded_by_id/{id}")]
        public async Task<IActionResult> GetLoadedById([FromRoute] string id)
        {
            return Ok(await service.GetLoadedById(id));
        }

        [HttpPost("get_loaded_collection")]
        public async Task<IActionResult> GetLoadedCollection([FromBody] GetCommentsRequest request)
        {
            return Ok(await service.GetLoadedCollection(request));
        }
    }
}
