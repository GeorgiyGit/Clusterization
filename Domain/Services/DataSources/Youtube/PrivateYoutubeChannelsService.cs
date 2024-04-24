using Domain.Interfaces.Other;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class PrivateYoutubeChannelsService : IPrivateYoutubeChannelsService
    {
        private readonly IRepository<Channel> _repository;
        public PrivateYoutubeChannelsService(IRepository<Channel> repository)
        {
            _repository = repository;
        }
        public async Task<Channel?> GetById(string id)
        {
            return await _repository.FindAsync(id);
        }
    }
}
