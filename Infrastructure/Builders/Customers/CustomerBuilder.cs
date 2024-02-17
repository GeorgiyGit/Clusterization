using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Customers
{
    public class CustomerBuilder
    {
        public static void CustomerBuild(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Profiles)
                        .WithOne(e => e.Owner)
                        .HasForeignKey(e => e.OwnerId);

            modelBuilder.HasMany(e => e.Workspaces)
                        .WithOne(e => e.Owner)
                        .HasForeignKey(e => e.OwnerId);

            modelBuilder.HasMany(e => e.Channels)
                        .WithOne(e => e.Loader)
                        .HasForeignKey(e => e.LoaderId);

            modelBuilder.HasMany(e => e.Comments)
                        .WithOne(e => e.Loader)
                        .HasForeignKey(e => e.LoaderId);
            
            modelBuilder.HasMany(e => e.Videos)
                        .WithOne(e => e.Loader)
                        .HasForeignKey(e => e.LoaderId);
        }
    }
}
