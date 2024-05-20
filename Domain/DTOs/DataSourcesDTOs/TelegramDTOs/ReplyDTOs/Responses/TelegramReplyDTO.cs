using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReactionDTOs;
using Domain.Entities.Customers;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Responses
{
    public class TelegramReplyDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? EditDate { get; set; }

        public long GroupedId { get; set; }
        public string Message { get; set; }

        public ICollection<TelegramReactionDTO> Reactions { get; set; }

        public DateTime LoadedDate { get; set; } = DateTime.UtcNow;
    }
}
