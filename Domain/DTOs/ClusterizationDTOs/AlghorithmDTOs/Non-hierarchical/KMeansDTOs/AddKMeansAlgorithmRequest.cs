using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs
{
    public class AddKMeansAlgorithmRequest
    {
        public int NumClusters { get; set; }
        public int Seed { get; set; }
    }
}
