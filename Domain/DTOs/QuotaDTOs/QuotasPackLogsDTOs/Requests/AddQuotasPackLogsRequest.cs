using Domain.Entities.Customers;
using Domain.Entities.Quotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets
{
    public class AddQuotasPackLogsRequest
    {
        public int PackId { get; set; }

        public string CustomerId { get; set; }
    }
}
