using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductReponsitories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex);
        Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex, Expression<Func<Product, bool>> expression);
        Task<IEnumerable<Product>> GetAllByCategoryId(int? pageSize, int? pageIndex,int? idCategory);
        Task<IEnumerable<Product>> GetAllByCategoryId(int? pageSize, int? pageIndex, int? id, string? search, string? branding,long priceMin, long priceMax);
        Task<int> CountAsyncById(int? id);
        Task<Product> GetByProductID(int? id);
        Task<int> CountByCateIdAsync(int? pageSize, int? pageIndex, int? id, string? search, string? branding, long priceMin, long priceMax);
    }
}
