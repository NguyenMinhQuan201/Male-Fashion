using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductReponsitories
{
    public class ProductReponsitories : ReponsitoryBase<Product>, IProductReponsitories
    {
        public ProductReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
