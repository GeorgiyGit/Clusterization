using Domain.Entities.DataSources.ExternalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DataSources.ExternalData
{
    internal class ExternalObjectsBuilder : IEntityTypeConfiguration<ExternalObject>
    {
        public void Configure(EntityTypeBuilder<ExternalObject> builder)
        {
            builder.HasKey(e => e.FullId);

            builder.HasOne(e => e.DataObject)
                   .WithOne(e => e.ExternalObject)
                   .HasForeignKey<ExternalObject>(e => e.DataObjectId)
                   .IsRequired(false);
        }
    }
}
