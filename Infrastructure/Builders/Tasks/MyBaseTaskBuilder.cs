using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Tasks;

namespace Infrastructure.Builders.Tasks
{
    internal class MyBaseTaskBuilder : IEntityTypeConfiguration<MyBaseTask>
    {
        public void Configure(EntityTypeBuilder<MyBaseTask> builder)
        {
            builder.HasOne(e => e.State)
                   .WithMany(e => e.Tasks)
                   .HasForeignKey(e => e.StateId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.Tasks)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasIndex(e => new { e.StartTime, e.EntityType, e.TaskType });
        }
    }
}
