using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms.Non_hierarchical
{
    [Route("api/[controller]")]
    [ApiController]
    public class KMeansAlgorithmController : AbstractClusterizationAlgorithmController<AddKMeansAlgorithmRequest, KMeansAlgorithmDTO>
    {
        public KMeansAlgorithmController(IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmRequest, KMeansAlgorithmDTO> service) : base(service)
        {
        }
    }
}
