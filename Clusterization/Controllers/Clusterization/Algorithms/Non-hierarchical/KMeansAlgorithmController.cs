using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms.Non_hierarchical
{
    [Route("api/[controller]")]
    [ApiController]
    public class KMeansAlgorithmController : AbstractClusterizationAlgorithmController<AddKMeansAlgorithmDTO, KMeansAlgorithmDTO>
    {
        public KMeansAlgorithmController(IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO, KMeansAlgorithmDTO> service) : base(service)
        {
        }
    }
}
