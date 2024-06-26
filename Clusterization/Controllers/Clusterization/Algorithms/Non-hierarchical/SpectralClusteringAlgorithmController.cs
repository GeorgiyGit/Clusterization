﻿using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.Interfaces.Clusterization.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Clusterization.Controllers.Clusterization.Algorithms.Non_hierarchical
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpectralClusteringAlgorithmController : AbstractClusterizationAlgorithmController<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO>
    {
        public SpectralClusteringAlgorithmController(IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO> service) : base(service)
        {
        }
    }
}
