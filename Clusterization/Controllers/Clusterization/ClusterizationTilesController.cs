﻿using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Interfaces.Clusterization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterizationTilesController : ControllerBase
    {
        private readonly IClusterizationTilesService service;
        public ClusterizationTilesController(IClusterizationTilesService service)
        {
            this.service = service;
        }

        [HttpPost("get_tile_by_profile")]
        public async Task<IActionResult> GetOneTileByProfile([FromBody] GetDisplayedPointsByProfileIdRequest request)
        {
            return Ok(await service.GetOneTile(request.ProfileId, request.X, request.Y, request.Z));
        }

        [HttpGet("get_tile_by_id/{id}")]
        public async Task<IActionResult> GetOneTileById([FromRoute] int id)
        {
            return Ok(await service.GetOneTile(id));
        }
    }
}
