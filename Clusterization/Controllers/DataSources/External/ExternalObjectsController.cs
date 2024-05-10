using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Interfaces.DataSources.ExternalData;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataSources.External
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalObjectsController : ControllerBase
    {
        private readonly IExternalDataObjectsService service;
        public ExternalObjectsController(IExternalDataObjectsService service)
        {
            this.service = service;
        }

        [HttpPost("get_collection")]
        public async Task<IActionResult> GetCollection([FromBody] GetExternalDataObjectsRequest request)
        {
            return Ok(await service.GetCollection(request));
        }
    }
}
