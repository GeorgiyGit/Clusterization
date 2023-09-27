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

        public ClusterizationColorValue Color { get; set; }
        public int ColorId { get; set; }
        
        public ICollection<ClusterizationEntity> Entities { get; set; } = new HashSet<ClusterizationEntity>();

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
