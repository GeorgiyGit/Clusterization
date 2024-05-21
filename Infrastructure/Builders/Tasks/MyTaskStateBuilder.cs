using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
