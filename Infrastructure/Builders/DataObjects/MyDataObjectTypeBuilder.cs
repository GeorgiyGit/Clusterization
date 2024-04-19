using Domain.Entities.DataObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DataObjects
{
    internal class MyDataObjectTypeBuilder : IEntityTypeConfiguration<MyDataObjectType>
    {
        public void Configure(EntityTypeBuilder<MyDataObjectType> builder)
        {
            builder.HasMany(e => e.DataObjects)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}
