
using Google.Apis.Http;

namespace Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses
{
    public class CustomerQuotasDTO
    {
        public int Id { get; set; }

        public int ExpiredCount { get; set; }
        public int AvailableCount { get; set; }

        public string TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
