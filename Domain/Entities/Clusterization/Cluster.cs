using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.DataObjects;
using System.Runtime.Serialization;

namespace Domain.Entities.Clusterization
{
    [DataContract(IsReference = true)]
    public class Cluster
    {
        public int Id { get; set; }

        public string Color { get; set; }

        public string? Name { get; set; }
        public int ChildElementsCount { get; set; }

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }

        public ICollection<Cluster> ChildClusters { get; set; } = new HashSet<Cluster>();

        public Cluster? ParentCluster { get; set; }
        public int? ParentClusterId { get; set; }

        public ICollection<MyDataObject> DataObjects { get; set; } = new HashSet<MyDataObject>();

        public ICollection<DisplayedPoint> DisplayedPoints { get; set; } = new HashSet<DisplayedPoint>();
    }
}
