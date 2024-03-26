
namespace Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses
{
    public class QuotasPackDTO
    {
        public int Id { get; set; }
        public ICollection<QuotasPackItemDTO> Items { get; set; } = new HashSet<QuotasPackItemDTO>();
    }
}
