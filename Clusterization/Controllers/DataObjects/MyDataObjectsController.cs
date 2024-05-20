using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.Interfaces.DataObjects;
using Domain.Interfaces.DataSources.ExternalData;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.DataObjects
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyDataObjectsController : ControllerBase
    {
        private readonly IMyDataObjectsService service;
        public MyDataObjectsController(IMyDataObjectsService service)
        {
            this.service = service;
        }

        [HttpGet("get_full_by_point/{pointId}")]
        public async Task<IActionResult> GetFullByDisplayedPointId([FromRoute] int pointId)
        {
            return Ok(await service.GetFullByDisplayedPointId(pointId));
        }
    }
}
