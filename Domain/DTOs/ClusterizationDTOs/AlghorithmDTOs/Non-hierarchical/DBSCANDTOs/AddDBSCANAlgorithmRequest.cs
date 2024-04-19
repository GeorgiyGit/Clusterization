using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs
{
    public class AddDBSCANAlgorithmRequest
    {
        public double Epsilon { get; set; }
        public int MinimumPointsPerCluster { get; set; }
    }
}
