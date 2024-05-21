using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Interfaces.Tasks;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.MyTasks
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Moderator)]
    public class ModeratorTasksController : ControllerBase
    {
        private readonly IModeratorTasksService service;
        public ModeratorTasksController(IModeratorTasksService service)
        {
            this.service = service;
        }

        [HttpPost("get_main_tasks")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> GetMainTasks([FromBody] ModeratorGetTasksRequest request)
        {
            return Ok(await service.GetMainTasks(request));
        }

        [HttpPost("get_sub_tasks")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> GetSubTasks([FromBody] ModeratorGetSubTasksRequest request)
        {
            return Ok(await service.GetSubTasks(request));
        }
    }
}
