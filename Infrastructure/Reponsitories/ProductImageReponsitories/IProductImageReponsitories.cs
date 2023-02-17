using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ProductImageReponsitories
{
    public interface IProductImageReponsitories : IReponsitoryBase<ProductImg>
    {
        public Task<IEnumerable<ProductImg>> GetAllProduct(int? id);
    }
}
