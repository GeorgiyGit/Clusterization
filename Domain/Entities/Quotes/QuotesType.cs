using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotes
{
    public class QuotesType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CustomerQuotes> CustomerQuotes { get; set; } = new HashSet<CustomerQuotes>();
        public ICollection<QuotesLogs> QuotesLogsCollection { get; set; } = new HashSet<QuotesLogs>();
    }
}
