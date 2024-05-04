using Domain.Entities.Customers;
using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Responses
{
    public class SimpleTelegramMessageDTO
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
        public string PostAuthor { get; set; }
        public int Views { get; set; }

        public DateTime LoadedDate { get; set; }

        public int TelegramRepliesCount { get; set; }

        public bool IsLoaded { get; set; } = true;
    }
}
