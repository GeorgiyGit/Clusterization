using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;

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
