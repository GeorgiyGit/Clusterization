using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotes
{
    public class QuotesPackLogs
    {
        public int Id { get; set; }

        public QuotesPack Pack { get; set; }
        public int PackId { get; set; }
    
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
