using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.RequestDTOs
{
    public class AddCommentsToWorkspaceByChannelRequest
    {
        public int WorkspaceId { get; set; }

        public int MaxCount { get; set; }
        public string ChannelId { get; set; }
        
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool IsVideoDateCount { get; set; } //load by video date, or by comments date
    }
}
