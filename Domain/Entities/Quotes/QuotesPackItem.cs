using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotes
{
    public class QuotesPackItem
    {
        public int Id { get; set; }
        public int Count { get; set; }

        public QuotesType Type { get; set; }
        public string TypeId { get; set; }

        public QuotesPack Pack { get; set; }
        public int PackId { get; set; }
    }
}
