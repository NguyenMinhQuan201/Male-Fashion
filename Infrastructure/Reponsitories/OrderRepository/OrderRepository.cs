using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.OrderReponsitory
{
    public class OrderReponsitory : RepositoryBase<Order>, IOrderRepository
    {
        public OrderReponsitory(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
