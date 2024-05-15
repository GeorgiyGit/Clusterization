using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.ExternalData;
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
    public class ExternalObjectsPacksController : ControllerBase
    {
        private readonly IExternalDataObjectsPacksService service;
        public ExternalObjectsPacksController(IExternalDataObjectsPacksService service)
        {
            this.service = service;
        }

        [HttpPost("load")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadExternalDataObjectBackgroundJob([FromForm] AddExternalDataRequest request)
        {
            await service.LoadExternalDataObjectBackgroundJob(request);
            return Ok();
        }

        [HttpPost("add")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddExternalDataObjectsToWorkspace([FromBody] AddExternalDataObjectsPacksToWorkspaceRequest request)
        {
            await service.AddExternalDataObjectsToWorkspace(request);
            return Ok();
        }
        [HttpPut("update")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdatePack([FromBody] UpdateExternalDataPackRequest request)
        {
            await service.UpdatePack(request);
            return Ok();
        }


        [HttpPost("load_and_add")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> LoadingExternalDataAndAddingToWorkspace([FromForm] AddExternalDataRequest request)
        {
            await service.LoadingExternalDataAndAddingToWorkspace(request);
            return Ok();
        }

        [HttpPost("get_collection")]
        public async Task<IActionResult> GetCollection([FromBody] GetExternalDataObjectsPacksRequest request)
        {
            return Ok(await service.GetCollection(request));
        }

        [HttpPost("get_customer_collection")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetCustomerCollection([FromBody] GetExternalDataObjectsPacksRequest request)
        {
            return Ok(await service.GetCustomerCollection(request));
        }

        [HttpGet("get_full/{id}")]
        public async Task<IActionResult> GetFullById([FromRoute] int id)
        {
            return Ok(await service.GetFullById(id));
        }
    }
}
