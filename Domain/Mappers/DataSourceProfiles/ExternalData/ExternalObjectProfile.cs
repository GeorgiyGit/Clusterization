﻿using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using Domain.Entities.DataSources.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DataSourceProfiles.ExternalData
{
    public class ExternalObjectProfile:AutoMapper.Profile
    {
        public ExternalObjectProfile()
        {
            CreateMap<ExternalObject, SimpleExternalObjectDTO>();
        }
    }
}
