using AutoMapper;
using Domain.Entities.Clusterization.FastClustering;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.Customers;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Telegram;
using Domain.Entities.DataSources.Youtube;
using Domain.Resources.Types.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tasks
{
    public class MyBaseTask
    {
        public string Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public MyTaskState State { get; set; }
        public string StateId { get; set; }

        public string? Description { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public float Percent { get; set; }
        public bool IsPercents { get; set; } = true;

        public string TaskType { get; set; } = TaskTypes.MainTask;

        public FastClusteringWorkflow? FastClusteringWorkflow { get; set; }
        public int? FastClusteringWorkflowId { get; set; }

        public ClusterizationProfile? ClusterizationProfile { get; set; }
        public int? ClusterizationProfileId { get; set; }

        public ClusterizationWorkspace? Workspace { get; set; }
        public int? WorkspaceId { get; set; }

        public WorkspaceDataObjectsAddPack? AddPack { get; set; }
        public int? AddPackId { get; set; }

        public YoutubeChannel? YoutubeChannel { get; set; }
        public string? YoutubeChannelId { get; set; }

        public YoutubeVideo? YoutubeVideo { get; set; }
        public string? YoutubeVideoId { get; set; }

        public TelegramChannel? TelegramChannel { get; set; }
        public long? TelegramChannelId { get; set; }

        public TelegramMessage? TelegramMessage { get; set; }
        public long? TelegramMessageId { get; set; }

        public ExternalObjectsPack? ExternalObjectsPack { get; set; }
        public int? ExternalObjectsPackId { get; set; }
    }
}
