﻿using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses;
using Domain.Entities.Quotas;

namespace Domain.Mappers.Quotas
{
    public class QuotasPackProfile : AutoMapper.Profile
    {
        public QuotasPackProfile()
        {
            CreateMap<QuotasPack, QuotasPackDTO>();
            CreateMap<QuotasPackItem, QuotasPackItemDTO>();

            CreateMap<QuotasPackLogs, QuotasPackLogsDTO>()
                .ForMember(dest => dest.Pack,
                           ost => ost.Ignore());
        }
    }
}
