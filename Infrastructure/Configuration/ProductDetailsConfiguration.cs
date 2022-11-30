using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ProductDetailsConfiguration : IEntityTypeConfiguration<ProductDetails>
    {
        public void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            builder.ToTable("ProductDetails");

            builder.HasKey(x => x.IdProduct);

            builder.Property(x => x.Price).HasColumnType("decimal(8, 2)");

            builder.Property(x => x.Discount).HasColumnType("decimal(8, 2)");

            builder.Property(x => x.Weight).HasColumnType("decimal(8, 2)");

            builder.HasOne<Product>(x => x.Product)
                .WithMany(g => g.ProductDetails)
                .HasForeignKey(s => s.IdProduct).IsRequired();
        }
    }
}
