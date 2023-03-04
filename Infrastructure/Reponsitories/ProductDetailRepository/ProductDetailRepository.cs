using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductDetailReponsitories
{
    public class ProductDetailReponsitories : RepositoryBase<ProductDetail>, IProductDetailReponsitories
    {
        public ProductDetailReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
