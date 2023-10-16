using Domain.Interfaces.Tasks;
using Domain.Interfaces.Youtube;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.MyTasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyTasksController : ControllerBase
    {
        private readonly IMyTasksService service;
        public MyTasksController(IMyTasksService service)
        {
            this.service = service;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> LoadAllTasks()
        {
            return Ok(await service.GetAllTasks());
        }
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var taskId = await service.CreateTask("test");
            for(float i = 0f; i < 100; i++)
            {
                Thread.Sleep(1000);
                await service.ChangeTaskPercent(taskId, i);
            }
            return Ok();
        }
    }
}
