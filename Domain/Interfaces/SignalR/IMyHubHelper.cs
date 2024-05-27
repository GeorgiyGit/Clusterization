using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.SignalR
{
    public interface IMyHubHelper
    {
        public Task Send(string groupName, object arg);
    }
}
