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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly MaleFashionDbContext _db;

        public OrderRepository(MaleFashionDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public async Task<Order> CreateAsyncFLByOrder(Order entity)
        {
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync();
            var noti = new Notifi
            {
                IsRead = false,
                Link = "order-list/edit/"+entity.IdOrder,
                Name = "Đơn hàng"
            };
            _db.Notifis.Add(noti);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
