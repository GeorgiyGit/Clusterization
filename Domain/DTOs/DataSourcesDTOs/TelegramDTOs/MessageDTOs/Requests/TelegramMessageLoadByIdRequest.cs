using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Requests
{
    public class TelegramMessageLoadByIdRequest
    {
        public int Id { get; set; }
        public long ChannelId { get; set; }
    }
}
