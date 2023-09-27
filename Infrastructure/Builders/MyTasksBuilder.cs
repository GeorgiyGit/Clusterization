using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders
{
    internal class MyTasksBuilder
    {
        public static void BuildAll(ModelBuilder modelBuilder)
        {
            TaskBuilder(modelBuilder.Entity<MyTask>());
            TaskStateBuilder(modelBuilder.Entity<MyTaskState>());
        }
        public static void TaskBuilder(EntityTypeBuilder<MyTask> modelBuilder)
        {
            modelBuilder.HasOne(e => e.State)
                        .WithMany(e => e.Tasks)
                        .HasForeignKey(e => e.StateId);
        }
        public static void TaskStateBuilder(EntityTypeBuilder<MyTaskState> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Tasks)
                        .WithOne(e => e.State)
                        .HasForeignKey(e => e.StateId);
        }
    }
}
