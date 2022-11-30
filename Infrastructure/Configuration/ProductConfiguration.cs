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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.IdProduct);

            builder.Property(x => x.IdProduct).ValueGeneratedOnAdd();

            builder.Property(x => x.Price).HasColumnType("decimal(8, 2)");


            builder.HasOne<Category>(x => x.Categories)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.IdCategory).IsRequired();

            builder.HasMany<Promotion>(x => x.Promotion)
                .WithMany(g => g.Product);

        }
    }
}
