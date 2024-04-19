using Domain.Interfaces.Other;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class PrivateYoutubeChannelsService : IPrivateYoutubeChannelsService
    {
        private readonly IRepository<Channel> repository;
        public PrivateYoutubeChannelsService(IRepository<Channel> repository)
        {
            this.repository = repository;
        }
        public async Task<Channel?> GetById(string id)
        {
            return await repository.FindAsync(id);
        }
    }
}
