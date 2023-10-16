using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Embeddings;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationWorkspacesController:ControllerBase
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


        [HttpGet("get_by_id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await service.GetFullById(id));
        }
        [HttpPost("get_workspaces")]
        public async Task<IActionResult> GetMany([FromBody] GetWorkspacesRequest request)
        {
            return Ok(await service.GetCollection(request));
        }


        [HttpPost("embedding_data/{id}")]
        public async Task<IActionResult> EmbeddingData([FromRoute] int id)
        {
            await embeddingsService.LoadEmbeddingsByWorkspace(id);
            return Ok();
        }
    }
}
