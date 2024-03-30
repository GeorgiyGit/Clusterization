using Domain.Entities.Customers;
using Domain.Entities.Quotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses
{
    public class QuotasLogsDTO
    {
        public int Id { get; set; }

        public QuotasTypeDTO Type { get; set; }

        public string CustomerId { get; set; }

        public int Count { get; set; }
    }
}
