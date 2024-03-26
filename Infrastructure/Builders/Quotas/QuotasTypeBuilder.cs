using Domain.Entities.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.Quotas
{
    internal class QuotasTypeBuilder : IEntityTypeConfiguration<QuotasType>
    {
        public void Configure(EntityTypeBuilder<QuotasType> builder)
        {
            builder.HasMany(e => e.CustomerQuotas)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);

            builder.HasMany(e => e.QuotasLogsCollection)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}
