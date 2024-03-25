using Domain.Entities.Quotes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Quotes
{
    internal class QuotesLogsBuilder : IEntityTypeConfiguration<QuotesLogs>
    {
        public void Configure(EntityTypeBuilder<QuotesLogs> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.QuotesLogsCollection)
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.QuotesLogsCollection)
                   .HasForeignKey(e => e.CustomerId);
        }
    }
}
