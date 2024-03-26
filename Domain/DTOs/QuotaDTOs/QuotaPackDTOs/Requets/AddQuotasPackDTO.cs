using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets
{
    public class AddQuotasPackDTO
    {
        public int Id { get; set; }
        public ICollection<AddQuotasPackItemDTO> Items { get; set; } = new HashSet<AddQuotasPackItemDTO>();
    }
}
