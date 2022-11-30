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
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(x => new { x.IdOrder, x.IdProduct });

            builder.Property(x => x.IdProduct).ValueGeneratedOnAdd();

            builder.Property(x => x.Price).HasColumnType("decimal(8, 2)");

            builder.Property(x => x.Discounnt).HasColumnType("decimal(8, 3)");

            builder.HasOne<Order>(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.IdOrder).IsRequired();

            builder.HasOne<Product>(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.IdProduct).IsRequired();
        }
    }
}
