using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    public class CustomerProfile:AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, SimpleCustomerDTO>();
        }
    }
}
