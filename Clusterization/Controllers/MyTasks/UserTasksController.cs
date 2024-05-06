﻿using Domain.DTOs.TaskDTOs;
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

        [HttpPost("get_collection")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetTasks([FromBody] CustomerGetTasksRequest request)
        {
            return Ok(await service.GetTasks(request));
        }

        [HttpGet("get_full/{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetFullTask([FromRoute] int id)
        {
            return Ok(await service.GetFullTask(id));
        }
    }
}
