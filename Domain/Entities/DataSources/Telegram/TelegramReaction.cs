using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DataSources.Telegram
{
    public class TelegramReaction
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public string Emotion { get; set; }

        public TelegramMessage? TelegramMessage { get; set; }
        public long? TelegramMessageId { get; set; }

        public TelegramReply? TelegramReply { get; set; }
        public long? TelegramReplyId { get; set; }

    }
}
