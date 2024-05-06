using Domain.DTOs.TaskDTOs;
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

        [HttpPost("get_all")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadAllTasks([FromBody] ModeratorGetTasksRequest request)
        {
            return Ok(await service.GetTasks(request));
        }
    }
}
