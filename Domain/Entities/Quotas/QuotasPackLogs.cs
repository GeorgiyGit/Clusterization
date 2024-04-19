using Domain.Entities.Customers;
using Domain.Entities.Monitorings;

namespace Domain.Entities.Quotas
{
    public class QuotasPackLogs : Monitoring
    {
        public int Id { get; set; }

        public QuotasPack Pack { get; set; }
        public int PackId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
