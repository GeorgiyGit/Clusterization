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
    internal class QuotesTypeBuilder : IEntityTypeConfiguration<QuotesType>
    {
        public void Configure(EntityTypeBuilder<QuotesType> builder)
        {
            builder.HasMany(e => e.CustomerQuotes)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);

            builder.HasMany(e => e.QuotesLogsCollection)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}
