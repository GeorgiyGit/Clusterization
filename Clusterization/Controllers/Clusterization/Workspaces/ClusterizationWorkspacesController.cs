﻿using Domain.DTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.DTOs.ExternalData;
using Domain.Interfaces.Clusterization.Workspaces;
using Domain.Interfaces.Embeddings;
using Domain.Resources.Types;
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
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Add([FromBody] AddClusterizationWorkspaceRequest model)
        {
            await service.Add(model);
            return Ok();
        }
        [HttpPut("update")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Update([FromBody] UpdateClusterizationWorkspaceRequest model)
        {
            await service.Update(model);
            return Ok();
        }

        [HttpPost]
        [Route("load_external_data")]
        [Authorize(Roles = UserRoles.User)]
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
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetCustomerCollection([FromBody] GetWorkspacesRequest request)
        {
            return Ok(await service.GetCustomerCollection(request));
        }
        [HttpPost("get_fast_clustering_collection")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetFastClusteringCollection([FromBody] PageParameters request)
        {
            return Ok(await service.GetFastClusteringCollection(request));
        }

        [HttpGet("get_entities/{id}"), DisableRequestSizeLimit]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetEntities([FromRoute] int id)
        {
            var list = await service.GetAllDataObjectsInList(id);

            var content = "";
            foreach (var item in list)
            {
                content += item.Replace("\n", "") + "\n";
            }

            string fileName = "entities.txt";

            var memory = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memory, Encoding.UTF8, 1024, true))
            {
                streamWriter.WriteLine(content);
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
    }
}
