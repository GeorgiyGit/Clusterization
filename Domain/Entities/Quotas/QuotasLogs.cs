using Domain.Entities.Customers;
using Domain.Entities.Monitorings;

namespace Domain.Entities.Quotas
{
    public class QuotasLogs : Monitoring
    {
        public string Id { get; set; }

        public QuotasType Type { get; set; }
        public string TypeId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public int Count { get; set; }
        public bool IsPlus { get; set; }
    }
}
