using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets
{
    public class AddQuotasPackRequest
    {
        public ICollection<AddQuotasPackItemRequest> Items { get; set; } = new HashSet<AddQuotasPackItemRequest>();
    }
}
