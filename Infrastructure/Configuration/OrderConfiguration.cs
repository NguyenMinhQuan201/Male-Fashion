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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.IdOrder);

            builder.Property(x => x.IdOrder).ValueGeneratedOnAdd();

            builder.Property(x => x.SumPrice).HasColumnType("decimal(8, 2)");

            builder.Property(x => x.Phone).HasColumnType("int");

        }
    }
}
