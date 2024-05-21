using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Tasks
{
    internal class MySubTaskBuilder : IEntityTypeConfiguration<MySubTask>
    {
        public void Configure(EntityTypeBuilder<MySubTask> builder)
        {
            builder.HasOne(e => e.GroupTask)
                   .WithMany(e => e.SubTasks)
                   .HasForeignKey(e => e.GroupTaskId);

            builder.HasIndex(e => new { e.Position });
        }
    }
}
