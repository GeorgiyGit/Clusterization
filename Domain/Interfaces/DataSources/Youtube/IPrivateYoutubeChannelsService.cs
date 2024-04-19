using Domain.Entities.DataSources.Youtube;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IPrivateYoutubeChannelsService
    {
        public Task<Channel?> GetById(string id);
    }
}
