using Domain.Interfaces.Youtube;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Youtube;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;

namespace Domain.Services.Youtube
{
    public class PrivateYoutubeChannelService : IPrivateYoutubeChannelService
    {
        private readonly IRepository<Channel> repository;
        public PrivateYoutubeChannelService(IRepository<Channel> repository)
        {
            this.repository = repository;
        }
        public async Task<Channel?> GetById(string id)
        {
            return await repository.FindAsync(id);
        }
    }
}
