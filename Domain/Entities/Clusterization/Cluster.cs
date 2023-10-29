using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class Cluster
    {
        public int Id { get; set; }

        public string Color { get; set; }
        
        public ICollection<ClusterizationEntity> Entities { get; set; } = new HashSet<ClusterizationEntity>();

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }

        public ICollection<Cluster> ChildClusters { get; set; } = new HashSet<Cluster>();

        public Cluster? ParentCluster { get; set; }
        public int? ParentClusterId { get; set; }

        public ICollection<DisplayedPoint> DisplayedPoints { get; set; }=new HashSet<DisplayedPoint>();
    }
}
