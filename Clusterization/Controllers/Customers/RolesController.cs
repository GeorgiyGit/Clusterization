using Domain.Interfaces.Customers;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService service;
        public RolesController(IRolesService service)
        {
            this.service = service;
        }

        [HttpPost("add_moderator/{id}")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> AddModerator([FromRoute] string id)
        {
            await service.AddModerator(id);
            return Ok();
        }


        [HttpPost("remove_moderator/{id}")]
        [Authorize(Roles = UserRoles.Moderator)]
        public async Task<IActionResult> RemoveModerator([FromRoute] string id)
        {
            await service.RemoveModerator(id);
            return Ok();
        }
    }
}
