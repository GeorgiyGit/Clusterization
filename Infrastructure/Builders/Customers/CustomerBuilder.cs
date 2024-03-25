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

            builder.HasMany(e => e.Channels)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.Comments)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.Videos)
                   .WithOne(e => e.Loader)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.QuotesLogsCollection)
                   .WithOne(e => e.Customer)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasMany(e => e.Quotes)
                   .WithOne(e => e.Customer)
                   .HasForeignKey(e => e.CustomerId);
        }
    }
}
