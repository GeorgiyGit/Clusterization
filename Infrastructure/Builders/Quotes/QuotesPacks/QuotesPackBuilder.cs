using Domain.Entities.Quotes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Quotes.QuotesPacks
{
    internal class QuotesPackBuilder : IEntityTypeConfiguration<QuotesPack>
    {
        public void Configure(EntityTypeBuilder<QuotesPack> builder)
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
