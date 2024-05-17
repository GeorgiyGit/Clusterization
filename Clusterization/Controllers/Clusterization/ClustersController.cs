using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests;
using Domain.Interfaces.Clusterization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClustersController : ControllerBase
    {
        private readonly IClustersService service;
        public ClustersController(IClustersService service)
        {
            this.service = service;
        }

        [HttpPost("get_cluster")]
        public async Task<IActionResult> GetClusters([FromBody] GetClustersRequest request)
        {
            return Ok(await service.GetClusters(request));
        }

        [HttpPost("get_cluster_entities")]
        public async Task<IActionResult> GetClusterEntities([FromBody] GetClusterDataRequest request)
        {
            return Ok(await service.GetClusterEntities(request));
        }
    }
}
