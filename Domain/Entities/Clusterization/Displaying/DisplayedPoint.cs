
using Domain.Entities.DataObjects;

namespace Domain.Entities.Clusterization.Displaying
{
    public class DisplayedPoint
    {
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public int OptimizationLevel { get; set; }

        public MyDataObject DataObject { get; set; }
        public long DataObjectId { get; set; }

        public ClusterizationTile Tile { get; set; }
        public int TileId { get; set; }

        public Cluster Cluster { get; set; }
        public int ClusterId { get; set; }
    }
}
