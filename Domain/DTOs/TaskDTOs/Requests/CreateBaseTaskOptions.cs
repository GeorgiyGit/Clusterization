using Domain.Entities.Clusterization.FastClustering;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Telegram;
using Domain.Entities.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Requests
{
    public class CreateBaseTaskOptions
    {
        public string? CustomerId { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }
        public string? Description { get; set; }

        public string StateId { get; set; }
        public bool IsPercents { get; set; } = true;

        public int? FastClusteringWorkflowId { get; set; }

        public int? ClusterizationProfileId { get; set; }
        public int? WorkspaceId { get; set; }

        public int? AddPackId { get; set; }

        public string? YoutubeChannelId { get; set; }
        public string? YoutubeVideoId { get; set; }

        public long? TelegramChannelId { get; set; }
        public long? TelegramMessageId { get; set; }

        public int? ExternalObjectsPackId { get; set; }
    }
}
