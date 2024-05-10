using Domain.Entities.DataSources.ExternalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DataSources.ExternalData
{
    internal class ExternalObjectBuilder : IEntityTypeConfiguration<ExternalObject>
    {
        public void Configure(EntityTypeBuilder<ExternalObject> builder)
        {
            builder.HasOne(e => e.DataObject)
                   .WithOne(e => e.ExternalObject)
                   .HasForeignKey<ExternalObject>(e => e.DataObjectId)
                   .IsRequired(false);

            builder.HasOne(e => e.Pack)
                   .WithMany(e => e.ExternalObjects)
                   .HasForeignKey(e => e.PackId);
        }
    }
}
