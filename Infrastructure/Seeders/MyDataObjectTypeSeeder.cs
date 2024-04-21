using Domain.Entities.DataObjects;
using Domain.Entities.EmbeddingModels;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class MyDataObjectTypeSeeder : IEntityTypeConfiguration<MyDataObjectType>
    {
        public void Configure(EntityTypeBuilder<MyDataObjectType> builder)
        {
            var comments = new MyDataObjectType()
            {
                Id = DataObjectTypes.Comment,
                Name = "Comment"
            };
            var externalData = new MyDataObjectType()
            {
                Id = DataObjectTypes.ExternalData,
                Name = "External Data"
            };

            builder.HasData(comments, externalData);
        }
    }
}
