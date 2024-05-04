using Domain.Resources.Types.DataSources.Telegram;
using Domain.Resources.Types.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Requests
{
    public class GetTelegramMessagesRequest
    {
        public string FilterStr { get; set; } = "";
        public long? ChannelId { get; set; }
        public string FilterType { get; set; } = TelegramMessageFilterTypes.ByTimeDesc;

        public PageParameters PageParameters { get; set; }
    }
}
