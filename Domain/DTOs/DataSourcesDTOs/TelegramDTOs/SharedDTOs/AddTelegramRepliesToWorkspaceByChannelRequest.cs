using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs
{
    public class AddTelegramRepliesToWorkspaceByChannelRequest
    {
        public int WorkspaceId { get; set; }

        public int MaxCount { get; set; }
        public long ChannelId { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool IsMessageDateCount { get; set; } //load by message date, or by replies date
    }
}
