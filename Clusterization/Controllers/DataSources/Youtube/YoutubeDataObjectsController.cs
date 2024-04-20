using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Interfaces.DataSources.Youtube;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.Youtube
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeDataObjectsController : ControllerBase
    {
        private readonly IYoutubeDataObjectsService service;
        public YoutubeDataObjectsController(IYoutubeDataObjectsService service)
        {
            this.service = service;
        }

        [HttpPost("add_comments_by_channel")]
        [Authorize]
        public async Task<IActionResult> AddCommentsByChannel([FromBody] AddCommentsToWorkspaceByChannelRequest request)
        {
            await this.service.LoadCommentsByChannel(request);
            return Ok();
        }

        [HttpPost("add_comments_by_videos")]
        [Authorize]
        public async Task<IActionResult> AddCommentsByVideos([FromBody] AddCommentsToWorkspaceByVideosRequest request)
        {
            await this.service.LoadCommentsByVideos(request);
            return Ok();
        }
    }
}
