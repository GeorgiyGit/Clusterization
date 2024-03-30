using Google.Apis.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests
{
    public class GetQuotasLogsRequest
    {
        public string? TypeId { get; set; }
        public PageParametersDTO PageParameters { get; set; }
    }
}
