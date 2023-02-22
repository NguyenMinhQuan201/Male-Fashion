using Infrastructure.Configuration;
using Infrastructure.Configurations;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class MaleFashionDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public MaleFashionDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API

            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new BlogImgConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new ImgConfiguration());
            modelBuilder.ApplyConfiguration(new ImportInvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new ImportInvoiceDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new IntroductionConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new QAConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());

            //Tạo bảng được cung cấp từ Identity
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImg> BlogImgs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Img> Imgs { get; set; }
        public DbSet<ImportInvoice> ImportInvoices { get; set; }
        public DbSet<ImportInvoiceDetails> ImportInvoiceDetails { get; set; }
        public DbSet<Introduction> Introductions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ProductImg> ProductImgs { get; set; }
        public DbSet<QA> QAs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }
    }
}
