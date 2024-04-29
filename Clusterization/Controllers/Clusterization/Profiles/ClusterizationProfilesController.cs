using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Profiles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationProfilesController : ControllerBase
    {
        private readonly IClusterizationProfilesService service;
        public ClusterizationProfilesController(IClusterizationProfilesService service)
        {
            this.service = service;
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddClusterizationProfileRequest model)
        {
            await service.Add(model);
            return Ok();
        }


        [HttpGet("get_full_by_id/{id}")]
        public async Task<IActionResult> GetFullById([FromRoute] int id)
        {
            return Ok(await service.GetFullById(id));
        }

        [HttpGet("get_simple_by_id/{id}")]
        public async Task<IActionResult> GetSimpleById([FromRoute] int id)
        {
            return Ok(await service.GetSimpleById(id));
        }

        [HttpPost("get_collection")]
        public async Task<IActionResult> GetCollection([FromBody] GetClusterizationProfilesRequest request)
        {
            return Ok(await service.GetCollection(request));
        }

        [HttpPost("get_customer_collection")]
        [Authorize]
        public async Task<IActionResult> GetCustomerCollection([FromBody] CustomerGetClusterizationProfilesRequest request)
        {
            return Ok(await service.GetCustomerCollection(request));
        }

        
        [HttpGet("calculate_quotas_count/{id}")]
        public async Task<IActionResult> CalculateQuotasCount([FromRoute] int id)
        {
            return Ok(await service.CalculateQuotasCount(id));
        }

        [HttpPost("elect/{id}")]
        [Authorize]
        public async Task<IActionResult> Elect([FromRoute] int id)
        {
            await service.Elect(id);
            return Ok();
        }

        [HttpPost("unelect/{id}")]
        [Authorize]
        public async Task<IActionResult> Unelect([FromRoute] int id)
        {
            await service.UnElect(id);
            return Ok();
        }
    }
}
