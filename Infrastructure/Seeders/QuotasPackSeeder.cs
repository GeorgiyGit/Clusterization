using Domain.Entities.Quotas;
using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class QuotasPackSeeder : IEntityTypeConfiguration<QuotasPack>
    {
        public void Configure(EntityTypeBuilder<QuotasPack> builder)
        {
            var basicPack = new QuotasPack()
            {
                Id = 1
            };

            var superPack = new QuotasPack()
            {
                Id = 2
            };

            builder.HasData(basicPack, superPack);
        }
    }
}
