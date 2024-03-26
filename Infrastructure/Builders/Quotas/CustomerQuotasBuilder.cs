using Domain.Entities.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.Quotas
{
    internal class CustomerQuotasBuilder : IEntityTypeConfiguration<CustomerQuotas>
    {
        public void Configure(EntityTypeBuilder<CustomerQuotas> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.CustomerQuotas)
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.Quotas)
                   .HasForeignKey(e => e.CustomerId);
        }
    }
}
