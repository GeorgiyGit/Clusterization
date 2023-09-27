using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationPointColors
    {
        public int Id { get; set; }

        public DisplayedPoint Point { get; set; }
        public int PointId { get; set; }

        public ClusterizationProfile Profile { get; set; }
        public int ProfileId { get; set; }

        public ICollection<ClusterizationColorValue> Colors { get; set; } = new HashSet<ClusterizationColorValue>();
    }
}
