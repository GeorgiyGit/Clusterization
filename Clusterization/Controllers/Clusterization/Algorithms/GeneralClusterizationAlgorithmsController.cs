using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Requests;
using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralClusterizationAlgorithmsController : ControllerBase
    {
        private readonly IGeneralClusterizationAlgorithmService service;
        public GeneralClusterizationAlgorithmsController(IGeneralClusterizationAlgorithmService service)
        {
            this.service = service;
        }

        [HttpGet("get_all/{typeId}")]
        public async Task<IActionResult> GetAllAlgorithmsByTypeId([FromRoute] string typeId)
        {
            return Ok(await service.GetAllAlgorithms(typeId));
        }

        [HttpPost("get_collection")]
        public async Task<IActionResult> GetAlgorithms([FromBody] GetAlgorithmsRequest request)
        {
            return Ok(await service.GetAlgorithms(request));
        }

        [HttpPost("calculate_quotas_count")]
        public async Task<IActionResult> CalculateQuotasCount([FromBody] CalculateQuotasCountRequest request)
        {
            return Ok(await service.CalculateQuotasCount(request.AlgorithmTypeId, (int)request.EntitiesCount, request.DimensionCount));
        }

        [HttpPost("calculate_quotas_count_by_workspace")]
        public async Task<IActionResult> CalculateQuotasCountByWorkspace([FromBody] CalculateQuotasCountRequest request)
        {
            return Ok(await service.CalculateQuotasCountByWorkspace(request.AlgorithmTypeId, (int)request.WorkspaceId, request.DimensionCount));
        }
    }
}
