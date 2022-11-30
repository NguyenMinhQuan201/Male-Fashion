using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.SupplierReponsitories
{
    public class SupplierReponsitories : ReponsitoryBase<Supplier>, ISupplierReponsitories
    {
        public SupplierReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
