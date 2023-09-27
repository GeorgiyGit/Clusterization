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
            await service.LoadCommentsFromVideo(options);
            return Ok();
        }

        [HttpGet("get_by_id/{id}")]
        public async Task<IActionResult> GetLoadedCommentById([FromRoute] string id)
        {
            return Ok(await service.GetLoadedCommentById(id));
        }

        [HttpPost("get_many")]
        public async Task<IActionResult> GetLoadedComments([FromBody] GetCommentsRequest request)
        {
            return Ok(await service.GetLoadedComments(request));
        }
    }
}
