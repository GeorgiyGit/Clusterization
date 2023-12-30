using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HelpModels
{
    public class TileGeneratingHelpModel
    {
        public ClusterizationEntity Entity { get; set; }
        public double[] EmbeddingValues { get; set; }
        public Cluster Cluster { get; set; }
    }
}
