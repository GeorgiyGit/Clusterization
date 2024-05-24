using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization.FastClustering
{
    public class FastClusteringWorkflow
    {
        public int Id { get; set; }
        
        public Customer Owner { get; set; }
        public string OwnerId { get; set; }

        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
    }
}
