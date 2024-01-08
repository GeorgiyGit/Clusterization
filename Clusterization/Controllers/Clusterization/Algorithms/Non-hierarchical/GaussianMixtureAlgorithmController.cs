using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms.Non_hierarchical
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaussianMixtureAlgorithmController : AbstractClusterizationAlgorithmController<AddGaussianMixtureAlgorithmDTO, GaussianMixtureAlgorithmDTO>
    {
        public GaussianMixtureAlgorithmController(IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmDTO, GaussianMixtureAlgorithmDTO> service) : base(service)
        {
        }
    }
}
