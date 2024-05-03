using Domain.Resources.Types.DataSources.Telegram;
using Domain.Resources.Types.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Requests
{
    public class GetTelegramChannelsRequest
    {
        public string FilterStr { get; set; } = "";
        public string FilterType { get; set; } = TelegramChannelFilterTypes.ByParticipantsDesc;

        public PageParameters PageParameters { get; set; }
    }
}
