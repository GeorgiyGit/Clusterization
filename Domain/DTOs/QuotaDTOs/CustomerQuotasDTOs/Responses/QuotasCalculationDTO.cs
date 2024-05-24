using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.QuotaDTOs.CustomerQuotasDTOs.Responses
{
    public class QuotasCalculationDTO
    {
        public QuotasTypeDTO Type { get; set; }
        public int Count { get; set; }
    }
}
