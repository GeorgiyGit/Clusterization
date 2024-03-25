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
    internal class MyTaskStateBuilder : IEntityTypeConfiguration<MyTaskState>
    {
        public void Configure(EntityTypeBuilder<MyTaskState> builder)
        {
            builder.HasMany(e => e.Tasks)
                   .WithOne(e => e.State)
                   .HasForeignKey(e => e.StateId);
        }
    }
}
