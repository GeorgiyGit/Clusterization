﻿using Domain.Entities.Monitorings;
using Domain.Entities.Quotas;
using Domain.Entities.Tasks;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataSources.Youtube;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.DataSources.Telegram;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.Clusterization.FastClustering;
using System.Runtime.Serialization;

namespace Domain.Entities.Customers
{
    [DataContract(IsReference = true)]
    public class Customer : IdentityUser, IMonitoring
    {
        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();


        public ICollection<YoutubeChannel> LoadedYoutubeChannels { get; set; } = new HashSet<YoutubeChannel>();
        public ICollection<YoutubeComment> LoadedYoutubeComments { get; set; } = new HashSet<YoutubeComment>();
        public ICollection<YoutubeVideo> LoadedYoutubeVideos { get; set; } = new HashSet<YoutubeVideo>();

        public ICollection<TelegramChannel> LoadedTelegramChannels { get; set; } = new HashSet<TelegramChannel>();
        public ICollection<TelegramMessage> LoadedTelegramMessages { get; set; } = new HashSet<TelegramMessage>();
        public ICollection<TelegramReply> LoadedTelegramReplies { get; set; } = new HashSet<TelegramReply>();

        public ICollection<ExternalObjectsPack> LoadedExternalObjectsPacks { get; set; } = new HashSet<ExternalObjectsPack>();

        public ICollection<CustomerQuotas> Quotas { get; set; } = new HashSet<CustomerQuotas>();
        public ICollection<QuotasLogs> QuotasLogsCollection { get; set; } = new HashSet<QuotasLogs>();

        public ICollection<QuotasPackLogs> QuotasPackLogsCollection { get; set; } = new HashSet<QuotasPackLogs>();

        public ICollection<MyBaseTask> Tasks { get; set; } = new HashSet<MyBaseTask>();

        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public DateTime? LastEditTime { get; set; }
        public DateTime? LastDeleteTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }

        public DateTime? LastEmailConfirmationSend { get; set; }

        public ICollection<WorkspaceDataObjectsAddPack> WorkspaceDataObjectsAddPacks { get; set; } = new HashSet<WorkspaceDataObjectsAddPack>();
    
        public FastClusteringWorkflow? FastClusteringWorkflow { get; set; }
        public int? FastClusteringWorkflowId { get; set; }
    }
}
