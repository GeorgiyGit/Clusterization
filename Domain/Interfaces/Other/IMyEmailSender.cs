using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Other
{
    public interface IMyEmailSender
    {
        public Task SendEmail(string subject, string body, string toUsername, string toEmail);
    }
}
