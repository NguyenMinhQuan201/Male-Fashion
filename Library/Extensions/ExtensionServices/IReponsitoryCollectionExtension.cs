using Infrastructure.Reponsitories.BaseReponsitory;
using Infrastructure.Reponsitories.BlogRepository;
using Infrastructure.Reponsitories.CategoryReponsitories;
using Infrastructure.Reponsitories.ImportInvoiceDetailsReponsitories;
using Infrastructure.Reponsitories.ImportInvoiceReponsitories;
using Infrastructure.Reponsitories.OrderDetailReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using Infrastructure.Reponsitories.ProductDetailReponsitories;
using Infrastructure.Reponsitories.ProductImageReponsitories;
using Infrastructure.Reponsitories.ProductReponsitories;
using Infrastructure.Reponsitories.SupplierReponsitories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Extensions.ExtensionServices
{
    public static class IReponsitoryCollectionExtension
    {
        public static IServiceCollection AddReponsitories(this IServiceCollection services)
        {
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductDetailReponsitories, ProductDetailReponsitories>();
            services.AddTransient<IProductImageRepository, ProductImageReponsitories>();
            services.AddTransient<IImportInvoiceDetailsRepository, ImportInvoiceDetailsReponsitories>();
            services.AddTransient<IImportInvoiceRepository, ImportInvoiceRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            return services;
        }
    }
}
