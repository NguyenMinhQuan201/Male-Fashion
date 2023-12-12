using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.OrderReponsitory
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order> CreateAsyncFLByOrder(Order entity);
        Task<List<Notifi>> GetAllNoti();
    }
}
