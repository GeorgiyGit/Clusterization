using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Youtube
{
    public interface IPrivateYoutubeChannelService
    {
        public Task<Channel?> GetById(string id);
    }
}
