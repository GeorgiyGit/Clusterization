using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HelpModels
{
    public class AddEmbeddingsWithDRHelpModel
    {
        public ClusterizationEntity Entity { get; set; }
        public double[] DataPoints { get; set; }
    }
}
