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
    public class UserTasksController : ControllerBase
    {
        private readonly IUserTasksService service;
        public UserTasksController(IUserTasksService service)
        {
            this.service = service;
        }

        [HttpPost("get_main_tasks")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetMainTasks([FromBody] CustomerGetTasksRequest request)
        {
            return Ok(await service.GetMainTasks(request));
        }

        [HttpPost("get_sub_tasks")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetSubTasks([FromBody] CustomerGetSubTasksRequest request)
        {
            return Ok(await service.GetSubTasks(request));
        }

        [HttpGet("get_full/{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetFullTask([FromRoute] string id)
        {
            return Ok(await service.GetFullTask(id));
        }
    }
}
