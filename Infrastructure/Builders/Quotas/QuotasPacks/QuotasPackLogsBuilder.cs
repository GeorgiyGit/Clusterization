using Domain.Entities.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.Quotas.QuotasPacks
{
    internal class QuotasPackLogsBuilder : IEntityTypeConfiguration<QuotasPackLogs>
    {
        public void Configure(EntityTypeBuilder<QuotasPackLogs> builder)
        {
            builder.HasOne(e => e.Pack)
                   .WithMany(e => e.Logs)
                   .HasForeignKey(e => e.PackId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.QuotasPackLogsCollection)
                   .HasForeignKey(e => e.CustomerId);
        }
    }
}
