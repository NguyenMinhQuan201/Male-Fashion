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
    public class BlogImgConfiguration : IEntityTypeConfiguration<BlogImg>
    {
        public void Configure(EntityTypeBuilder<BlogImg> builder)
        {
            builder.ToTable("BlogImgs");

            builder.HasKey(x => x.IdBlogImg);

            builder.Property(x => x.IdBlogImg).ValueGeneratedOnAdd();

            builder.HasOne<Blog>(x => x.Blog)
                .WithMany(x => x.BlogImgs)
                .HasForeignKey(x => x.IdBlog).IsRequired();
        }
    }
}
