using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using Infrastructure.Reponsitories.ProductDetailReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductImageReponsitories
{
    public class ProductImageReponsitories : ReponsitoryBase<ProductImg>, IProductImageReponsitories
    {
        private readonly MaleFashionDbContext _db;

        public ProductImageReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<ProductImg>> GetAllProduct(int? id)
        {
            var query = _db.ProductImgs.Where(x=>x.ProductId==id).AsQueryable();
            var pageCount = query.Count();
            return query.ToList();
        }
    }
}
