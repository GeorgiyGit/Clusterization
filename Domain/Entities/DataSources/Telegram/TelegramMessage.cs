using Domain.Entities.Customers;
using Domain.Entities.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DataSources.Telegram
{
    public class TelegramMessage
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public int TelegramID { get; set; }
        public DateTime? EditDate { get; set; }

        public string Message { get; set; }
        public string? PostAuthor { get; set; }
        public int Views { get; set; }

        public DateTime LoadedDate { get; set; } = DateTime.UtcNow;

        public TelegramChannel TelegramChannel { get; set; }
        public long TelegramChannelId { get; set; }

        public int TelegramRepliesCount { get; set; }

        public ICollection<TelegramReply> TelegramReplies { get; set; } = new HashSet<TelegramReply>();
        public ICollection<TelegramReaction> Reactions { get; set; } = new HashSet<TelegramReaction>();

        public Customer Loader { get; set; }
        public string LoaderId { get; set; }

        public MyDataObject? DataObject { get; set; }
        public long? DataObjectId { get; set; }
    }
}
