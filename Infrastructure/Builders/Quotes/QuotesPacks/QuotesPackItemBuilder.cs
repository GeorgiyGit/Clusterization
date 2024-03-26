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
    internal class QuotesPackItemBuilder : IEntityTypeConfiguration<QuotesPackItem>
    {
        public void Configure(EntityTypeBuilder<QuotesPackItem> builder)
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
