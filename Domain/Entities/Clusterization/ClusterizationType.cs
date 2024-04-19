using Domain.Entities.Clusterization.Workspaces;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
    }
}
