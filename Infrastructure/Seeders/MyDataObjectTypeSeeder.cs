using Domain.Entities.DataObjects;
using Domain.Entities.EmbeddingModels;
using Domain.Resources.Types;
using Domain.Resources.Types.DataSources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class MyDataObjectTypeSeeder : IEntityTypeConfiguration<MyDataObjectType>
    {
        public void Configure(EntityTypeBuilder<MyDataObjectType> builder)
        {
            var youtubeComments = new MyDataObjectType()
            {
                Id = DataObjectTypes.YoutubeComment,
                Name = "Youtube Comment"
            };

            var telegramMessages = new MyDataObjectType()
            {
                Id = DataObjectTypes.TelegramMessage,
                Name = "Telegram Message"
            };
            var telegramReplies = new MyDataObjectType()
            {
                Id = DataObjectTypes.TelegramReply,
                Name = "Telegram Reply"
            };

            var externalData = new MyDataObjectType()
            {
                Id = DataObjectTypes.ExternalData,
                Name = "External Data"
            };

            builder.HasData(youtubeComments,telegramMessages,telegramReplies, externalData);
        }
    }
}
