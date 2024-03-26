using Domain.Entities.Quotes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Quotes.QuotesPacks
{
    internal class QuotesPackLogsBuilder : IEntityTypeConfiguration<QuotesPackLogs>
    {
        public void Configure(EntityTypeBuilder<QuotesPackLogs> builder)
        {
            builder.HasOne(e => e.Pack)
                   .WithMany(e => e.Logs)
                   .HasForeignKey(e => e.PackId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.QuotesPackLogsCollection)
                   .HasForeignKey(e => e.CustomerId);
        }
    }
}
