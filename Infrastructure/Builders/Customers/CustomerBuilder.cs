using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Customers
{
    internal class CustomerBuilder : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.Owner)
                   .HasForeignKey(e => e.OwnerId);

            builder.HasMany(e => e.Workspaces)
                   .WithOne(e => e.Owner)
                   .HasForeignKey(e => e.OwnerId);

            builder.HasMany(e => e.LoadedYoutubeChannels)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.LoadedYoutubeComments)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.LoadedYoutubeVideos)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);


            builder.HasMany(e => e.LoadedTelegramChannels)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.LoadedTelegramMessages)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.LoadedTelegramReplies)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.QuotasLogsCollection)
                   .WithOne(e => e.Customer)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasMany(e => e.Quotas)
                   .WithOne(e => e.Customer)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasMany(e => e.QuotasPackLogsCollection)
                   .WithOne(e => e.Customer)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasMany(e => e.Tasks)
                   .WithOne(e => e.Customer)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasMany(e => e.WorkspaceDataObjectsAddPacks)
                   .WithOne(e => e.Owner)
                   .HasForeignKey(e => e.OwnerId);

            builder.HasMany(e => e.LoadedExternalObjectsPacks)
                   .WithOne(e => e.Owner)
                   .HasForeignKey(e => e.OwnerId);
        }
    }
}
