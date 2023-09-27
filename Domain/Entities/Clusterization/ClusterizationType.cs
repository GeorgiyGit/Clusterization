using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
    }
}
