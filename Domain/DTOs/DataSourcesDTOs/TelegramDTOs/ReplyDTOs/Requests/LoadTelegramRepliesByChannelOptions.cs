using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Requests
{
    public class LoadTelegramRepliesByChannelOptions
    {
        public long ChannelId { get; set; }
        public DateTime? DateFrom { get; set; } //Load from that date
        public DateTime? DateTo { get; set; } //Load to that date

        public int MaxLoad { get; set; }

        public int MaxLoadForOneMessage { get; set; }
    }
}
