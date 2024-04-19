using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests
{
    public class AddQuotasToCustomerRequest
    {
        public int PackId { get; set; }
        public string CustomerId { get; set; }
    }
}
