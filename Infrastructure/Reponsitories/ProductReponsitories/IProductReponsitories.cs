using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductReponsitories
{
    public interface IProductReponsitories : IReponsitoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex);
        Task<IEnumerable<Product>> GetAllByCategoryId(int? pageSize, int? pageIndex);
        Task<IEnumerable<Product>> GetAllByCategoryId(int? pageSize, int? pageIndex,int ?id);
        Task<int> CountAsyncById(int? id);
        Task<Product> GetByIdFixed(int? id);
    }
}
