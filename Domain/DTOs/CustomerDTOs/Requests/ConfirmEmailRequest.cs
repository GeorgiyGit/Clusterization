using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.CustomerDTOs.Requests
{
    public class ConfirmEmailRequest
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
