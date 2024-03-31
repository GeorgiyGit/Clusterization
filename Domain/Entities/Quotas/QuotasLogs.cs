﻿using Domain.Entities.Customers;
using Domain.Entities.Monitorings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quotas
{
    public class QuotasLogs : Monitoring
    {
        public string Id { get; set; }

        public QuotasType Type { get; set; }
        public string TypeId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public int Count { get; set; }
    }
}
