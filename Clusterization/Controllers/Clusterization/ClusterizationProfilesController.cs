using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
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
        public async Task<IActionResult> Add([FromBody] AddClusterizationProfileDTO model)
        {
            await service.Add(model);
            return Ok();
        }


        [HttpGet("get_by_id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await service.GetFullById(id));
        }

        [HttpPost("get_collection")]
        public async Task<IActionResult> GetCollection([FromBody] GetClusterizationProfilesRequestDTO request)
        {
            return Ok(await service.GetCollection(request));
        }
    }
}
