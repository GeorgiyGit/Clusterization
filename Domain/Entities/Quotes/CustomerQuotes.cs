using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotes
{
    public class CustomerQuotes
    {
        public int Id { get; set; }

        public int ExpiredCount { get; set; }
        public int AvailableCount { get; set; }

        public QuotesType Type { get; set; }
        public string TypeId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
