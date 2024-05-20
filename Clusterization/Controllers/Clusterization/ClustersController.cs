using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests;
using Domain.Interfaces.Clusterization;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TL;

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

        [HttpPost("get_clusters_file"), DisableRequestSizeLimit]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetEntities([FromBody] GetClustersFileRequest request)
        {
            var fileModel = await service.GetClustersFileModel(request);


            string fileName = "clusters.txt";
            string jsonString = JsonSerializer.Serialize(fileModel, new JsonSerializerOptions { WriteIndented = true });

            var memory = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memory, Encoding.UTF8, 1024, true))
            {
                await streamWriter.WriteAsync(jsonString);
            }
            memory.Position = 0;
            return File(memory, "text/plain", fileName);
        }
    }
}
