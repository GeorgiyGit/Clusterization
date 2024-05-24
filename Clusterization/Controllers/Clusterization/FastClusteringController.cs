using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.FastClusteringDTOs.Requests;
using Domain.Interfaces.Clusterization;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class FastClusteringController : ControllerBase
    {
        private readonly IFastClusteringService service;
        public FastClusteringController(IFastClusteringService service)
        {
            this.service = service;
        }

        [HttpPost("create_workflow")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> CreateWorkflow()
        {
            return Ok(await service.CreateWorkflow());
        }

        [HttpPost("initialize_workspace")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> InitializeWorkspace([FromBody] FastClusteringInitialRequest request)
        {
            return Ok(await service.InitializeWorkspace(request));
        }

        [HttpPost("initialize_profile")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> InitializeProfile([FromBody] FastClusteringProcessRequest request)
        {
            return Ok(await service.InitializeProfile(request));
        }
    }
}
