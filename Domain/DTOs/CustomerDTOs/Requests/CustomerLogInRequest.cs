using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.CustomerDTOs.Requests
{
    public class CustomerLogInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
