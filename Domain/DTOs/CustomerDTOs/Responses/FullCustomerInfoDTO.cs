using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.CustomerDTOs.Responses
{
    public class FullCustomerInfoDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsModerator { get; set; }
        public bool IsUser { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
