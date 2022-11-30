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
    public class IntroductionConfiguration : IEntityTypeConfiguration<Introduction>
    {
        public void Configure(EntityTypeBuilder<Introduction> builder)
        {
            builder.ToTable("Introductions");

            builder.HasKey(x => x.IdIntro);

            builder.Property(x => x.IdIntro).ValueGeneratedOnAdd();
        }
    }
}
