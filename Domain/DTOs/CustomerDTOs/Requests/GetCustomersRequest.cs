using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.CustomerDTOs.Requests
{
    public class GetCustomersRequest
    {
        public string? FilterStr { get; set; } = "";
        public PageParametersDTO PageParameters { get; set; }
    }
}
