
namespace Domain.Entities.Quotas
{
    public class QuotasType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CustomerQuotas> CustomerQuotas { get; set; } = new HashSet<CustomerQuotas>();
        public ICollection<QuotasLogs> QuotasLogsCollection { get; set; } = new HashSet<QuotasLogs>();
        public ICollection<QuotasPackItem> PackItems { get; set; } = new HashSet<QuotasPackItem>();
    }
}
