using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotes
{
    public class QuotesPack
    {
        public int Id { get; set; }

        public ICollection<QuotesPackItem> Items { get; set; } = new HashSet<QuotesPackItem>();
        public ICollection<QuotesPackLogs> Logs { get; set; } = new HashSet<QuotesPackLogs>();
    }
}
