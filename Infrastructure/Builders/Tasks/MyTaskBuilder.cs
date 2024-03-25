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
    internal class MyTaskBuilder : IEntityTypeConfiguration<MyTask>
    {
        public void Configure(EntityTypeBuilder<MyTask> builder)
        {
            builder.HasOne(e => e.State)
                   .WithMany(e => e.Tasks)
                   .HasForeignKey(e => e.StateId);
        }
    }
}
