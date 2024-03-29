using Domain.Entities.Clusterization;
using Domain.Entities.Monitorings;
using Domain.Entities.Quotas;
using Domain.Entities.Tasks;
using Domain.Entities.Youtube;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Customers
{
    public class Customer : IdentityUser, IMonitoring
    {
        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();


        public ICollection<Channel> Channels { get; set; }=new HashSet<Channel>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Video> Videos { get; set; } = new HashSet<Video>();

        public ICollection<CustomerQuotas> Quotas { get; set; } = new HashSet<CustomerQuotas>();
        public ICollection<QuotasLogs> QuotasLogsCollection { get; set; } = new HashSet<QuotasLogs>();

        public ICollection<QuotasPackLogs> QuotasPackLogsCollection { get; set; } = new HashSet<QuotasPackLogs>();

        public ICollection<MyTask> Tasks { get; set; } = new HashSet<MyTask>();

        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public DateTime? LastEditTime { get; set; }
        public DateTime? LastDeleteTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
    }
}
