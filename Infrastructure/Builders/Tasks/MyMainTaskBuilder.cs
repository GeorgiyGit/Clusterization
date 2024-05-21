using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Tasks
{
    internal class MyMainTaskBuilder : IEntityTypeConfiguration<MyMainTask>
    {
        public void Configure(EntityTypeBuilder<MyMainTask> builder)
        {
            builder.HasMany(e => e.SubTasks)
                   .WithOne(e => e.GroupTask)
                   .HasForeignKey(e => e.GroupTaskId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
