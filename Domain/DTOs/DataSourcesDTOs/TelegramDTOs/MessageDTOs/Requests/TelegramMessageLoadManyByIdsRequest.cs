using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Requests
{
    public class TelegramMessageLoadManyByIdsRequest
    {
        public ICollection<int> Ids { get; set; }
        public long ChannelId { get; set; }
    }
}
