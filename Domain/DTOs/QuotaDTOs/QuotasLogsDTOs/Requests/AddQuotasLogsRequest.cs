using Domain.Entities.Customers;
using Domain.Entities.Quotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests
{
    public class AddQuotasLogsRequest
    {
        public string Id { get; set; }

        public string TypeId { get; set; }
        public string CustomerId { get; set; }

        public int Count { get; set; }
    }
}
