using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.GetEntitiesTaskDTOs;
using Domain.Interfaces.Tasks;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.MyTasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesTasksController : ControllerBase
    {
        private readonly IEntitiesTasksService service;
        public EntitiesTasksController(IEntitiesTasksService service)
        {
            this.service = service;
        }

        [HttpPost("get_workspace_tasks")]
        public async Task<IActionResult> GetWorkspaceTasks([FromBody] GetEntityTasksRequest<int> request)
        {
            return Ok(await service.GetWorkspaceTasks(request));
        }

        [HttpPost("get_profile_tasks")]
        public async Task<IActionResult> GetProfileTasks([FromBody] GetEntityTasksRequest<int> request)
        {
            return Ok(await service.GetProfileTasks(request));
        }

        [HttpPost("get_youtube_channel_tasks")]
        public async Task<IActionResult> GetYoutubeChannelTasks([FromBody] GetEntityTasksRequest<string> request)
        {
            return Ok(await service.GetYoutubeChannelTasks(request));
        }
        [HttpPost("get_youtube_video_tasks")]
        public async Task<IActionResult> GetYoutubeVideoTasks([FromBody] GetEntityTasksRequest<string> request)
        {
            return Ok(await service.GetYoutubeVideoTasks(request));
        }

        [HttpPost("get_telegram_channel_tasks")]
        public async Task<IActionResult> GetTelegramChannelTasks([FromBody] GetEntityTasksRequest<long> request)
        {
            return Ok(await service.GetTelegramChannelTasks(request));
        }
        [HttpPost("get_telegram_message_tasks")]
        public async Task<IActionResult> GetTelegramMessageTasks([FromBody] GetEntityTasksRequest<long> request)
        {
            return Ok(await service.GetTelegramMessageTasks(request));
        }

        [HttpPost("get_external_pack_tasks")]
        public async Task<IActionResult> GetExternalObjectsPackTasks([FromBody] GetEntityTasksRequest<int> request)
        {
            return Ok(await service.GetExternalObjectsPackTasks(request));
        }
    }
}
