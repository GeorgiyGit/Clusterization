using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.DTOs.ExternalData;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Embeddings;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationWorkspacesController : ControllerBase
    {
        private readonly IClusterizationWorkspacesService service;
        private readonly ILoadEmbeddingsService embeddingsService;
        public ClusterizationWorkspacesController(IClusterizationWorkspacesService service,
                                                  ILoadEmbeddingsService embeddingsService)
        {
            this.service = service;
            this.embeddingsService = embeddingsService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddClusterizationWorkspaceDTO model)
        {
            await service.Add(model);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateClusterizationWorkspaceDTO model)
        {
            await service.Update(model);
            return Ok();
        }

        [HttpPost("add_comments_by_channel")]
        public async Task<IActionResult> AddCommentsByChannel([FromBody] AddCommentsToWorkspaceByChannelRequest request)
        {
            await service.LoadCommentsByChannel(request);
            return Ok();
        }
        [HttpPost("add_comments_by_videos")]
        public async Task<IActionResult> AddCommentsByVideos([FromBody] AddCommentsToWorkspaceByVideosRequest request)
        {
            await service.LoadCommentsByVideos(request);
            return Ok();
        }

        [HttpPost]
        [Route("load_external_data")]
        public async Task<IActionResult> LoadExternalData([FromForm] AddExternalDataDTO data)
        {
            await service.LoadExternalData(data);
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


        [HttpPost]
        [Route("load_embedding_data/{id}")]
        public async Task<IActionResult> LoadEmbeddingData([FromRoute] int id)
        {
            await embeddingsService.LoadEmbeddingsByWorkspace(id);
            return Ok();
        }
    }
}
