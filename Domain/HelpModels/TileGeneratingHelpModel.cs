using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using Domain.Entities.DataObjects;

namespace Domain.HelpModels
{
    public class TileGeneratingHelpModel
    {
        public MyDataObject DataObject { get; set; }
        public double[] EmbeddingValues { get; set; }
        public Cluster Cluster { get; set; }
    }
}
