﻿using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms
{

    public abstract class AbstractClusterizationAlgorithmController<AddDTO, GetDTO> : ControllerBase
    {
        private readonly IAbstractClusterizationAlgorithmService<AddDTO, GetDTO> service;
        public AbstractClusterizationAlgorithmController(IAbstractClusterizationAlgorithmService<AddDTO, GetDTO> service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddAlgorithm([FromBody] AddDTO model)
        {
            await service.AddAlgorithm(model);
            return Ok();
        }

        [HttpPost]
        [Route("cluster_data/{profileId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> ClusterData([FromRoute] int profileId)
        {
            await service.ClusterData(profileId);
            return Ok();
        }
    }
}
