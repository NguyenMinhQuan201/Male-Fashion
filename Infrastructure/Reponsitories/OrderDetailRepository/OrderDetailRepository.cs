using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.OrderDetailReponsitory
{
    public class OrderDetailRepository : RepositoryBase<OrderDetails>, IOrderDetailRepository
    {
        public OrderDetailRepository(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
