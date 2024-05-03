using Domain.Entities.Customers;
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
        public long TelegramID { get; set; }
        public DateTime? EditDate { get; set; }
        //Entities
        public string Flags { get; set; }
        public string Flags2 { get; set; }
        public int Forwards { get; set; }
        //from_boosts_applied
        //from_id
        //fwd_from
        //media !important
        public string Message { get; set; }
        //post_author
        public int Views { get; set; }

        public DateTime LoadedDate { get; set; } = DateTime.UtcNow;

        public TelegramChannel TelegramChannel { get; set; }
        public long TelegramChannelId { get; set; }

        public int TelegramRepliesCount { get; set; }

        public ICollection<TelegramReply> TelegramReplies { get; set; } = new HashSet<TelegramReply>();
        public ICollection<TelegramReaction> Reactions { get; set; } = new HashSet<TelegramReaction>();

        public Customer Loader { get; set; }
        public string LoaderId { get; set; }
    }
}
