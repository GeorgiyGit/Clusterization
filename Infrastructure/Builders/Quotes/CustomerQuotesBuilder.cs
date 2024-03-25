using Domain.Entities.Customers;
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
    internal class CustomerQuotesBuilder : IEntityTypeConfiguration<CustomerQuotes>
    {
        public void Configure(EntityTypeBuilder<CustomerQuotes> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.CustomerQuotes)
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.Customer)
                   .WithMany(e => e.Quotes)
                   .HasForeignKey(e => e.CustomerId);
        }
    }
}
