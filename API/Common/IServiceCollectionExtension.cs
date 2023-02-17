using Domain.Common.FileStorage;
using Domain.Features.Category;
using Domain.Features.Color;
using Domain.Features.Discount;
using Domain.Features.ManageSuppliers;
using Domain.Features.Product;
using Domain.Features.Role;
using Domain.Features.Size;
using Domain.Features.Supplier;
using Domain.IServices.User;

namespace API.Common
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMyLibraryServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServiceRole, ServiceRole>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<ISizeService, SizeService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IStorageService, FileStorageService>();
            return services;
        }
    }
}
