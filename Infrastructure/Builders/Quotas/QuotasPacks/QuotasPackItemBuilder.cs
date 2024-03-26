using Domain.Entities.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.Quotas.QuotasPacks
{
    internal class QuotasPackItemBuilder : IEntityTypeConfiguration<QuotasPackItem>
    {
        public void Configure(EntityTypeBuilder<QuotasPackItem> builder)
        {
            builder.HasOne(e => e.Pack)
                   .WithMany(e => e.Items)
                   .HasForeignKey(e => e.PackId);

            builder.HasOne(e => e.Type)
                   .WithMany(e => e.PackItems)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}
