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
    public class ImportInvoiceConfiguration : IEntityTypeConfiguration<ImportInvoice>
    {
        public void Configure(EntityTypeBuilder<ImportInvoice> builder)
        {
            builder.ToTable("ImportInvoices");

            builder.HasKey(x => x.IdImportInvoice);

            builder.Property(x => x.IdImportInvoice).ValueGeneratedOnAdd();

            builder.Property(x => x.SumPrice).HasColumnType("decimal(8, 2)");

            builder.Property(x => x.Phone).HasColumnType("tinyint");

            builder.HasOne<Supplier>(x => x.Supplier)
                .WithMany(x => x.ImportInvoice)
                .HasForeignKey(x => x.IdSupplier);
        }
    }
}
