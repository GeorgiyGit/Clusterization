using Domain.Entities.Clusterization;
using Domain.Entities.Quotes;
using Domain.Entities.Youtube;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Customers
{
    public class Customer : IdentityUser
    {
        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();


        public ICollection<Channel> Channels { get; set; }=new HashSet<Channel>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Video> Videos { get; set; } = new HashSet<Video>();

        public ICollection<CustomerQuotes> Quotes { get; set; } = new HashSet<CustomerQuotes>();
        public ICollection<QuotesLogs> QuotesLogsCollection { get; set; } = new HashSet<QuotesLogs>();
    }
}
