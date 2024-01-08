using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms.Non_hierarchical
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBSCANAlgorithmController : AbstractClusterizationAlgorithmController<AddDBScanAlgorithmDTO, DBScanAlgorithmDTO>
    {
        public DBSCANAlgorithmController(IAbstractClusterizationAlgorithmService<AddDBScanAlgorithmDTO, DBScanAlgorithmDTO> service) : base(service)
        {
        }
    }
}
