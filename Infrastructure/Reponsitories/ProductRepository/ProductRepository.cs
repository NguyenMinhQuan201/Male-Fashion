using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductReponsitories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly MaleFashionDbContext _db ;
        public ProductRepository(MaleFashionDbContext dbContext) : base(dbContext)
        {
            _db= dbContext;
        }

        public async Task<int> CountAsyncById(int? id)
        {
            var query = from p in _db.Products
                        join c in _db.Categories on p.IdCategory equals c.IdCategory
                        where c.IdCategory == id
                        select p;
            var pageCount = query.Count();
            return pageCount;
        }

        public async Task<int> CountByCateIdAsync(int? idCategory)
        {
            var query = from p in _db.Products
                        join c in _db.Categories on p.IdCategory equals c.IdCategory
                        where c.IdCategory == idCategory && p.Status == true
                        select p;
            var pageCount = query.Count();
            return pageCount;
        }

        public async Task<IEnumerable<Product>> GetAllByCategoryId(int? pageSize, int? pageIndex, int? idCategory)
        {
            var query = from p in _db.Products.Include(x => x.ProductImgs).AsQueryable()
                        join c in _db.Categories on p.IdCategory equals c.IdCategory
                        where c.IdCategory == idCategory && p.Status==true
                        select p;
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }
        public async Task<IEnumerable<Product>> GetAllByCategoryId(int? pageSize, int? pageIndex,int? idCategory,string ? search)
        {
            var query = from p in _db.Products.Include(x => x.ProductImgs).AsQueryable()
                        join c in _db.Categories on p.IdCategory equals c.IdCategory
                        where c.IdCategory == idCategory && c.Name.Contains(search) && p.Status == true
                        select p;
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }

        public async Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex)
        {
            var query = _db.Products.Include(x => x.ProductImgs).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();

        }
        public async Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex, Expression<Func<Product, bool>> expression)
        {
            var query = _db.Products.Include(x => x.ProductImgs).Where(expression).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }

        public async Task<Product> GetByProductID(int? id)
        {
            var query = _db.Products.Include(x => x.ProductImgs).Where(x => x.IdProduct == id).FirstOrDefault();
            return query;
            
        }
    }
}
