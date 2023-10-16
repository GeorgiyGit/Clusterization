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
    }
}
