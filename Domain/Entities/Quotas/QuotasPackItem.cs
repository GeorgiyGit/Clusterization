
namespace Domain.Entities.Quotas
{
    public class QuotasPackItem
    {
        public int Id { get; set; }
        public int Count { get; set; }

        public QuotasType Type { get; set; }
        public string TypeId { get; set; }

        public QuotasPack Pack { get; set; }
        public int PackId { get; set; }
    }
}
