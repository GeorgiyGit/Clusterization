using Domain.Entities.Customers;

namespace Domain.Entities.Quotas
{
    public class CustomerQuotas
    {
        public int Id { get; set; }

        public int ExpiredCount { get; set; }
        public int AvailableCount { get; set; }

        public QuotasType Type { get; set; }
        public string TypeId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
