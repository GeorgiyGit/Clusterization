using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Responses
{
    public class SimpleTelegramChannelDTO
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }

        public long PhotoId { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }
        public int ParticipantsCount { get; set; }
        
        public int TelegramMessagesCount { get; set; }
        public int TelegramRepliesCount { get; set; }

        public DateTime LoadedDate { get; set; }
        public DateTime Date { get; set; }

        public bool IsLoaded { get; set; } = true;
    }
}
