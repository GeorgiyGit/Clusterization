using Domain.Entities.Clusterization;
using Domain.Resources.Types;
using Domain.Resources.Types.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders.Clusterization
{
    internal class ClusterizationTypeSeeder : IEntityTypeConfiguration<ClusterizationType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationType> builder)
        {
            var youtubeComments = new ClusterizationType()
            {
                Id = ClusterizationTypes.YoutubeComments,
                Name = "Youtube Comments"//"Коментарі"
            };

            var telegramMessages = new ClusterizationType()
            {
                Id = ClusterizationTypes.TelegramMessages,
                Name = "Telegram Messages"
            };
            var telegramReplies = new ClusterizationType()
            {
                Id = ClusterizationTypes.TelegramReplies,
                Name = "Telegram Replies"
            };

            var external = new ClusterizationType()
            {
                Id = ClusterizationTypes.External,
                Name = "From file"//"З файлу"
            };

            builder.HasData(youtubeComments,
                telegramMessages,
                telegramReplies,
                external);
        }
    }
}
