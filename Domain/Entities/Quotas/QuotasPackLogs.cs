using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Entities.Customers;
using Domain.Entities.Monitorings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotas
{
    public class QuotasPackLogs : Monitoring
    {
        public int Id { get; set; }

        public QuotasPack Pack { get; set; }
        public int PackId { get; set; }
    
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
