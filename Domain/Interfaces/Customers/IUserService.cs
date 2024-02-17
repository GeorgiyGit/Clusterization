using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Customers
{
    public interface IUserService
    {
        internal Task<string?> GetCurrentUserId();
    }
}
