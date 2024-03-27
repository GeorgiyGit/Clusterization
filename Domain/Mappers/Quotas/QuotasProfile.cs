using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.Entities.Quotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.Quotas
{
    public class QuotasProfile:AutoMapper.Profile
    {
        public QuotasProfile()
        {
            CreateMap<CustomerQuotas, CustomerQuotasDTO>();
        }
    }
}
