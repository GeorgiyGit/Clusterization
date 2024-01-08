using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs
{
    public class SpectralClusteringAlgorithmDTO : AbstractAlgorithmDTO
    {
        public int NumClusters { get; set; }
        public double Gamma { get; set; }
    }
}
