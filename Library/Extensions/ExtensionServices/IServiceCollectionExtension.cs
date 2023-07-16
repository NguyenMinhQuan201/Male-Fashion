using Domain.Common.FileStorage;
using Domain.Features;
using Domain.Features.Category;
using Domain.Features.Discount;
using Domain.Features.Order;
using Domain.Features.Product;
using Domain.Features.Role;
using Domain.Features.Supplier;
using Domain.IServices.User;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Extensions.ExtensionServices
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServiceRole, ServiceRole>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IOperationService, OperationService>();
            /*services.AddTransient<IRoleOperationService, RoleOperationService>();
            services.AddTransient<IUserOperationService, UserOperationService>();*/
            return services;
        }
    }
}
