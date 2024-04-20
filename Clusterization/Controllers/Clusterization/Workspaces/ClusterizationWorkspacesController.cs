using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.DTOs.ExternalData;
using Domain.Interfaces.Clusterization.Workspaces;
using Domain.Interfaces.Embeddings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Clusterization.Controllers.Clusterization.Workspaces
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationWorkspacesController : ControllerBase
    {
        private readonly IClusterizationWorkspacesService service;
        public ClusterizationWorkspacesController(IClusterizationWorkspacesService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddClusterizationWorkspaceRequest model)
        {
            await service.Add(model);
            return Ok();
        }
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateClusterizationWorkspaceRequest model)
        {
            await service.Update(model);
            return Ok();
        }

        [HttpPost("add_comments_by_channel")]
        [Authorize]
        public async Task<IActionResult> AddCommentsByChannel([FromBody] AddCommentsToWorkspaceByChannelRequest request)
        {
            return Ok();
        }
        [HttpPost("add_comments_by_videos")]
        [Authorize]
        public async Task<IActionResult> AddCommentsByVideos([FromBody] AddCommentsToWorkspaceByVideosRequest request)
        {
            return Ok();
        }

        [HttpPost]
        [Route("load_external_data")]
        [Authorize]
        public async Task<IActionResult> LoadExternalData([FromForm] AddExternalDataRequest data)
        {
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
        public async Task<IActionResult> GetCollection([FromBody] GetWorkspacesRequest request)
        {
            return Ok(await service.GetCollection(request));
        }

        [HttpPost("get_customer_collection")]
        [Authorize]
        public async Task<IActionResult> GetCustomerCollection([FromBody] GetWorkspacesRequest request)
        {
            return Ok(await service.GetCustomerCollection(request));
        }

        [HttpGet("get_entities/{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> GetEntities([FromRoute] int id)
        {
            var list = await service.GetAllDataObjectsInList(id);

            // Your logic to get data or generate content
            var content = "";
            foreach (var item in list)
            {
                content += item.Replace("\n", "") + "\n";
            }

            // Replace "generated_text_file.txt" with your desired file name
            string fileName = "entities.txt";

            var memory = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memory, Encoding.UTF8, 1024, true))
            {
                // Write content line by line
                streamWriter.WriteLine(content);
                // Add more lines as needed
            }
            memory.Position = 0;
            return File(memory, "text/plain", fileName);
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }


        /*[HttpPost]
        [Route("load_embedding_data/{id}")]
        [Authorize]
        public async Task<IActionResult> LoadEmbeddingData([FromRoute] int id)
        {
            await embeddingsService.LoadEmbeddingsByWorkspace(id);
            return Ok();
        }*/
    }
}
