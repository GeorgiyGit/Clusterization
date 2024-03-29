using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.CustomerDTOs.Responses
{
    public class SimpleCustomerDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsModerator { get; set; }
    }
}
