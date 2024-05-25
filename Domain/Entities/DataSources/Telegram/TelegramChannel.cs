using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DataSources.Telegram
{
    public class TelegramChannel
    {
        public long Id { get; set; }
        public long TelegramID { get; set; }

        public bool IsActive { get; set; }

        public string About { get; set; }
        public long PhotoId { get; set; }

        public long ParticipantsCount { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public int TelegramMessagesCount { get; set; }
        public int TelegramRepliesCount { get; set; }

        public DateTime LoadedDate { get; set; } = DateTime.UtcNow;
        public DateTime Date { get; set; }

        public ICollection<TelegramMessage> TelegramMessages { get; set; } = new HashSet<TelegramMessage>();
        public ICollection<TelegramReply> TelegramReplies { get; set; } = new HashSet<TelegramReply>();

        public Customer Loader { get; set; }
        public string LoaderId { get; set; }

        public ICollection<MyBaseTask> Tasks { get; set; } = new HashSet<MyBaseTask>();
    }
}
