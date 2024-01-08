using Domain.Entities.Clusterization;
using Domain.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Algorithms
{
    public interface IClusterizationAlgorithmsHelpService
    {
        public Task<List<AddEmbeddingsWithDRHelpModel>> CreateHelpModels(ClusterizationProfile profile);
    }
}
