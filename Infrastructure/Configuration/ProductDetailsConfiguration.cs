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
    public class ProductDetailsConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.ToTable("ProductDetails");

            builder.HasKey(x => x.Id);

            builder.HasOne<Product>(x => x.Product)
                .WithMany(g => g.ProductDetails)
                .HasForeignKey(s => s.ProductId).IsRequired();




        }
    }
}
