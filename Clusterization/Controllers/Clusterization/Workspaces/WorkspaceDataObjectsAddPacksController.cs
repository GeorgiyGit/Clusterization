using Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Requests;
using Domain.Interfaces.Clusterization.Workspaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Workspaces
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceDataObjectsAddPacksController : ControllerBase
    {
        private readonly IWorkspaceDataObjectsAddPacksService service;
        public WorkspaceDataObjectsAddPacksController(IWorkspaceDataObjectsAddPacksService service)
        {
            this.service = service;
        }

        [HttpPost("get_simple_list")]
        public async Task<IActionResult> GetSimplePacks([FromBody] GetWorkspaceDataObjectsAddPacksRequest request)
        {
            return Ok(await service.GetSimplePacks(request));
        }

        [HttpGet("get_simple_by_id/{id}")]
        public async Task<IActionResult> GetSimplePackById([FromRoute] int id)
        {
            return Ok(await service.GetSimplePackById(id));
        }

        [HttpPost("get_customer_simple_list")]
        [Authorize]
        public async Task<IActionResult> GetCustomerSimplePacks([FromBody] GetCustomerWorkspaceDataObjectsAddPacksRequest request)
        {
            return Ok(await service.GetCustomerSimplePacks(request));
        }

        [HttpGet("get_full/{id}")]
        public async Task<IActionResult> GetFullPack([FromRoute] int id)
        {
            return Ok(await service.GetFullPack(id));
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePack([FromRoute] int id)
        {
            await service.DeletePack(id);
            return Ok();
        }

        [HttpPost("restore/{id}")]
        [Authorize]
        public async Task<IActionResult> RestorePack([FromRoute] int id)
        {
            await service.RestorePack(id);
            return Ok();
        }
    }
}
