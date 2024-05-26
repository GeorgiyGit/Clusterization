
namespace Domain.DTOs.TaskDTOs
{
    public class FullTaskDTO : TaskDTO
    {
        public string? Description { get; set; }
        public string CustomerId { get; set; }

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
