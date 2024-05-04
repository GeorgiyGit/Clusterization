using Domain.Entities.DataObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.DataObjects
{
    internal class MyDataObjectBuilder : IEntityTypeConfiguration<MyDataObject>
    {
        public void Configure(EntityTypeBuilder<MyDataObject> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.DataObjects)
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.YoutubeComment)
                   .WithOne(e => e.DataObject)
                   .HasForeignKey<MyDataObject>(e => e.YoutubeCommentId)
                   .IsRequired(false);

            builder.HasOne(e => e.ExternalObject)
                   .WithOne(e => e.DataObject)
                   .HasForeignKey<MyDataObject>(e => e.ExternalObjectId)
                   .IsRequired(false);

            builder.HasMany(e => e.EmbeddingObjectsGroups)
                   .WithOne(e => e.DataObject)
                   .HasForeignKey(e => e.DataObjectId);

            builder.HasMany(e => e.WorkspaceDataObjectsAddPacks)
                   .WithMany(e => e.DataObjects);

            builder.HasMany(e => e.Workspaces)
                   .WithMany(e => e.DataObjects);

            builder.HasMany(e => e.Clusters)
                   .WithMany(e => e.DataObjects);

            builder.HasMany(e => e.DisplayedPoints)
                   .WithOne(e => e.DataObject)
                   .HasForeignKey(e => e.DataObjectId);

            builder.HasOne(e => e.TelegramMessage)
                   .WithOne(e => e.DataObject)
                   .HasForeignKey<MyDataObject>(e => e.TelegramMessageId)
                   .IsRequired(false);

            builder.HasOne(e => e.TelegramReply)
                   .WithOne(e => e.DataObject)
                   .HasForeignKey<MyDataObject>(e => e.TelegramReplyId)
                   .IsRequired(false);
        }
    }
}
