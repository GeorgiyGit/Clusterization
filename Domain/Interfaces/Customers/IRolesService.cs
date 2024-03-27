using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Customers
{
    public interface IRolesService
    {
        public Task AddModerator(string id);
        public Task RemoveModerator(string id);
    }
}
