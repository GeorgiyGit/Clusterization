using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Requests
{
    public class ChangeTaskReferencesRequest
    {
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
