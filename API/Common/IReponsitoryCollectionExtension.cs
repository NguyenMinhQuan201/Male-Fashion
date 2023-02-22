using Infrastructure.Reponsitories.BaseReponsitory;
using Infrastructure.Reponsitories.CategoryReponsitories;
using Infrastructure.Reponsitories.ColorReponsitories;
using Infrastructure.Reponsitories.ImportInvoiceDetailsReponsitories;
using Infrastructure.Reponsitories.ImportInvoiceReponsitories;
using Infrastructure.Reponsitories.ProductDetailReponsitories;
using Infrastructure.Reponsitories.ProductImageReponsitories;
using Infrastructure.Reponsitories.ProductReponsitories;
using Infrastructure.Reponsitories.SizeReponsitories;
using Infrastructure.Reponsitories.SupplierReponsitories;

namespace API.Common
{
    public static class IReponsitoryCollectionExtension
    {
        public static IServiceCollection AddMyLibraryReponsitories(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<ISupplierReponsitories, SupplierReponsitories>();
            services.AddTransient<IColorReponsitories, ColorReponsitories>();
            services.AddTransient<ISizeReponsitories, SizeReponsitories>();
            services.AddTransient<IProductReponsitories, ProductReponsitories>();
            services.AddTransient<ICategoryReponsitories, CategoryReponsitories>();
            services.AddTransient<IProductDetailReponsitories, ProductDetailReponsitories>();
            services.AddTransient<IProductImageReponsitories, ProductImageReponsitories>();
            services.AddTransient<IImportInvoiceDetailsReponsitories,ImportInvoiceDetailsReponsitories>();
            services.AddTransient<IImportInvoiceReponsitories, ImportInvoiceReponsitories>();
            return services;
        }
    }
}
