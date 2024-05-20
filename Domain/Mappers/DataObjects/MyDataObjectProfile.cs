using Domain.DTOs.DataObjectDTOs.Responses;
using Domain.Entities.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DataObjects
{
    public class MyDataObjectProfile:AutoMapper.Profile
    {
        public MyDataObjectProfile()
        {
            CreateMap<MyDataObject, FullDataObjectDTO>();
        }
    }
}
