using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms.Non_hierarchical
{

    public abstract class AbstractClusterizationAlgorithmController<AddDTO,GetDTO> : ControllerBase
    {
        private readonly IAbstractClusterizationAlgorithmService<AddDTO,GetDTO> service;
        public AbstractClusterizationAlgorithmController(IAbstractClusterizationAlgorithmService<AddDTO, GetDTO> service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddAlgorithm([FromBody] AddDTO model)
        {
            await service.AddAlgorithm(model);
            return Ok();
        }
    }
}
