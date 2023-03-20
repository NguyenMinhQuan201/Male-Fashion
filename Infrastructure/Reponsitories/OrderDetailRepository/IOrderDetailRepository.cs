using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.OrderDetailReponsitory
{
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetails>
    {
    }
}
