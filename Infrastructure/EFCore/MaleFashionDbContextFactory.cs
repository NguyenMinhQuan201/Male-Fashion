using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructureta2.EF
{
    // File này để tạo kết nối với sql thông qua file appsetting
    public class MaleFashionDbContextFactory : IDesignTimeDbContextFactory<MaleFashionDbContext>
    {
        public MaleFashionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MaleFashionDb");


            var optionsBuilder = new DbContextOptionsBuilder<MaleFashionDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MaleFashionDbContext(optionsBuilder.Options);
        }
    }
}
