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
    public class QAConfiguration : IEntityTypeConfiguration<QA>
    {
        public void Configure(EntityTypeBuilder<QA> builder)
        {
            builder.ToTable("QA");

            builder.HasKey(x => x.IdQA);

            builder.Property(x => x.IdQA).ValueGeneratedOnAdd();
        }
    }
}
