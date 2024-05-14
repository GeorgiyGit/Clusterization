using Domain.Entities.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.Quotas
{
    internal class QuotasLogsBuilder : IEntityTypeConfiguration<QuotasLogs>
    {
        public void Configure(EntityTypeBuilder<QuotasLogs> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.QuotasLogsCollection)
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.QuotasLogsCollection)
                   .HasForeignKey(e => e.CustomerId);

            builder.HasIndex(e => new { e.CreationTime });
        }
    }
}
