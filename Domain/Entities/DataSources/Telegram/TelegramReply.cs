using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DataSources.Telegram
{
    public class TelegramReply
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long TelegramID { get; set; }

        public DateTime? EditDate { get; set; }

        public long GroupedId { get; set; }
        public string Message { get; set; }

        public TelegramMessage TelegramMessage { get; set; }
        public long TelegramMessageId { get; set; }

        public TelegramChannel TelegramChannel { get; set; }
        public long TelegramChannelId { get; set; }

        public Customer Loader { get; set; }
        public string LoaderId { get; set; }

        public ICollection<TelegramReaction> Reactions { get; set; } = new HashSet<TelegramReaction>();

        public DateTime LoadedDate { get; set; } = DateTime.UtcNow;
    }
}
