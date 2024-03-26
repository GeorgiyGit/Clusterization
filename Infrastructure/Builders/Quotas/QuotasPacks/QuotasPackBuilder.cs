using Domain.Entities.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.Quotas.QuotasPacks
{
    internal class QuotasPackBuilder : IEntityTypeConfiguration<QuotasPack>
    {
        public void Configure(EntityTypeBuilder<QuotasPack> builder)
        {
            builder.HasMany(e => e.Items)
                   .WithOne(e => e.Pack)
                   .HasForeignKey(e => e.PackId);

            builder.HasMany(e => e.Logs)
                   .WithOne(e => e.Pack)
                   .HasForeignKey(e => e.PackId);
        }
    }
}
