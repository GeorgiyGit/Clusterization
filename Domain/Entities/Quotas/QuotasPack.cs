using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotas
{
    public class QuotasPack
    {
        public int Id { get; set; }

        public ICollection<QuotasPackItem> Items { get; set; } = new HashSet<QuotasPackItem>();
        public ICollection<QuotasPackLogs> Logs { get; set; } = new HashSet<QuotasPackLogs>();
    }
}
