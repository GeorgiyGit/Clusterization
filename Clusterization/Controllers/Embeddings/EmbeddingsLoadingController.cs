using Domain.DTOs.EmbeddingDTOs.Requests;
using Domain.Interfaces.EmbeddingModels;
using Domain.Interfaces.Embeddings.EmbeddingsLoading;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Embeddings
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbeddingsLoadingController : ControllerBase
    {
        private readonly IEmbeddingsLoadingService service;
        public EmbeddingsLoadingController(IEmbeddingsLoadingService service)
        {
            this.service = service;
        }

        [HttpPost("load_by_pack")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadEmbeddingsByWorkspaceDataPack([FromBody] LoadEmbeddingsByWorkspaceDataPackRequest request)
        {
            await service.LoadEmbeddingsByWorkspaceDataPack(request.PackId, request.EmbeddingModelId);
            return Ok();
        }

        [HttpPost("load_by_profile/{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadEmbeddingsByProfile([FromRoute] int id)
        {
            await service.LoadEmbeddingsByProfile(id);
            return Ok();
        }
    }
}
