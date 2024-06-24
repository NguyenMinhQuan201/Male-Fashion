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
            _db.Orders.Add(entity); //lưu order vào sql
            await _db.SaveChangesAsync();//lưu vào sql
            var noti = new Notifi
            {
                IsRead = false,
                Link = "order-list/edit/" + entity.IdOrder,
                Name = "Đơn hàng",
                Price = entity.SumPrice,
                Time = DateTime.Now,
            };
            _db.Notifis.Add(noti);
            await _db.SaveChangesAsync();//lưu thông báo vào sql
            return entity;
        }

        public async Task<List<Notifi>> GetAllNoti()
        {
            return _db.Notifis.OrderByDescending(x=>x.Id).ToList();
        }

        public async Task<bool> UpdateNoti(Notifi obj)
        {
            _db.Notifis.Update(obj);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
