using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Resources.Types;
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
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddCommentsByChannel([FromBody] AddYoutubeCommentsToWorkspaceByChannelRequest request)
        {
            await this.service.LoadCommentsByChannel(request);
            return Ok();
        }

        [HttpPost("add_comments_by_videos")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddCommentsByVideos([FromBody] AddYoutubeCommentsToWorkspaceByVideosRequest request)
        {
            await this.service.LoadCommentsByVideos(request);
            return Ok();
        }
    }
}
