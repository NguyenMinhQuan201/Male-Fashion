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
    public class ImportInvoiceDetailsConfiguration : IEntityTypeConfiguration<ImportInvoiceDetails>
    {
        public void Configure(EntityTypeBuilder<ImportInvoiceDetails> builder)
        {
            builder.ToTable("ImportInvoiceDetails");

            builder.HasKey(x => new { x.IdImportInvoice, x.IdProduct });

            builder.Property(x => x.IdProduct).ValueGeneratedOnAdd();

            builder.Property(x => x.Price).HasColumnType("decimal(8, 2)");

            builder.Property(x => x.Discount).HasColumnType("tinyint");

            builder.Property(x => x.Weight).HasColumnType("decimal(8, 2)");

            builder.HasOne<ImportInvoice>(x => x.ImportInvoice)
                .WithMany(x => x.ImportInvoiceDetails)
                .HasForeignKey(x => x.IdImportInvoice).IsRequired();

            builder.HasOne<Product>(x => x.Product)
                .WithMany(x => x.ImportInvoiceDetails)
                .HasForeignKey(x => x.IdProduct);

        }
    }
}
