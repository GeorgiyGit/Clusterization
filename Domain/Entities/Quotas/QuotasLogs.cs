using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotas
{
    public class QuotasLogs
    {
        public int Id { get; set; }
        
        public QuotasType Type { get; set; }
        public string TypeId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
