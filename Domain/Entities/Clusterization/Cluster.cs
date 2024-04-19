using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.DataObjects;

namespace Domain.Entities.Clusterization
{
    public class Cluster
    {
        public int Id { get; set; }

        public string Color { get; set; }


        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }

        public ICollection<Cluster> ChildClusters { get; set; } = new HashSet<Cluster>();

        public Cluster? ParentCluster { get; set; }
        public int? ParentClusterId { get; set; }

        public ICollection<MyDataObject> DataObjects { get; set; } = new HashSet<MyDataObject>();

        public ICollection<DisplayedPoint> DisplayedPoints { get; set; } = new HashSet<DisplayedPoint>();
    }
}
