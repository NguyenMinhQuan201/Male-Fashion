using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.OrderDetailReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using System.Linq.Expressions;

namespace Domain.Features.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDetailRepository _orderDetailReponsitory;
        private readonly IOrderRepository _orderReponsitory;
        public OrderService(IOrderRepository orderReponsitory, IOrderDetailRepository orderDetailReponsitory)
        {
            _orderDetailReponsitory = orderDetailReponsitory;
            _orderReponsitory = orderReponsitory;
        }
        public async Task<ApiResult<bool>> Create(OrderDto request)
        {
            var order = new Infrastructure.Entities.Order()
            {
                Status = request.Status,
                SumPrice = 0,
                Address = request.Address,
                CreatedAt = DateTime.Now,
                DeliveryAt = DateTime.Now,
                Email = request.Email,
                NameCustomer = request.NameCustomer,
                Note = request.Note,
                Payments = request.Payments,
                Phone = request.Phone,
            };
            var temp = new List<OrderDetails>();
            if (request.OrderDetails != null)
            {
                foreach (var orderDetail in request.OrderDetails)
                {
                    var _productOrder = new OrderDetails()
                    {
                        IdProduct=orderDetail.IdProduct,
                        CreatedAt = DateTime.Now,
                        DeliveryAt  = DateTime.Now,
                        Discounnt = orderDetail.Discounnt,
                        Price = orderDetail.Price,
                        Quantity = orderDetail.Quantity,
                    };
                    temp.Add(_productOrder);
                }
                order.OrderDetails = temp;
            }
            try
            {
                await _orderReponsitory.CreateAsync(order);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.ToString());
            }
            return new ApiSuccessResult<bool>();
        }

        public Task<ApiResult<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<GetOrderDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _orderReponsitory.CountAsync();
            var query = await _orderReponsitory.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Order, bool>> expression = x => x.NameCustomer.Contains(search);
                query = await _orderReponsitory.GetAll(pageSize, pageIndex, expression);
                totalRow = await _orderReponsitory.CountAsync(expression);
            }
            //Paging
            /*totalRow = query.Result.Count();*/
            var data = query
                .Select(x => new GetOrderDto()
                {
                   IdOrder=x.IdOrder,
                   Status=x.Status,
                   SumPrice=x.SumPrice,
                   NameCustomer=x.NameCustomer,
                   Address=x.Address,
                   Phone=x.Phone,
                   Email=x.Email,
                   CreatedAt=x.CreatedAt,
                   DeliveryAt=x.DeliveryAt,
                   FinishAt=x.FinishAt,
                   Note=x.Note,
                   Payments=x.Payments,
                }).ToList();
            var pagedResult = new PagedResult<GetOrderDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetOrderDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetOrderDto>>(pagedResult);
        }

        public async Task<ApiResult<List<OrderDetailDto>>> GetAllOrderDetail(int id)
        {
            Expression<Func<Infrastructure.Entities.OrderDetails, bool>> expression = x => x.IdOrder==id;
            var query = await _orderDetailReponsitory.GetByCondition(expression);
            var data = query
                .Select(x => new OrderDetailDto()
                {
                    IdOrder=x.IdOrder,
                    IdProduct=x.IdProduct,
                    Discounnt=x.Discounnt,
                    Quantity=x.Quantity,
                    Price=x.Price,
                }).ToList();
            if (data == null)
            {
                return new ApiErrorResult<List<OrderDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<List<OrderDetailDto>>(data);
        }

        public Task<ApiResult<PagedResult<GetOrderDto>>> GetAllPagingRemoved(int? pageSize, int? pageIndex, string? search)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOrderDto>GetById(int id)
        {
            var findobj = await _orderReponsitory.GetById(id);
            if (findobj == null)
            {
                return null;
            }

            var obj = new GetOrderDto()
            {
                IdOrder = findobj.IdOrder,
                Status = findobj.Status,
                SumPrice = findobj.SumPrice,
                NameCustomer = findobj.NameCustomer,
                Address = findobj.Address,
                Phone = findobj.Phone,
                Email = findobj.Email,
                CreatedAt = findobj.CreatedAt,
                DeliveryAt = findobj.DeliveryAt,
                FinishAt = findobj.FinishAt,
                Note = findobj.Note,
                Payments = findobj.Payments,
            };
            return obj;
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, OrderDto request)
        {
            if (id != null)
            {
                var findobj = await _orderReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = request.Status;
                findobj.SumPrice = request.SumPrice;
                findobj.NameCustomer = request.NameCustomer;
                findobj.Address = request.Address;
                findobj.Email = request.Email;
                findobj.Phone = request.Phone;
                findobj.Payments = request.Payments;
                findobj.DeliveryAt = request.DeliveryAt;
                if (findobj.Status == true)
                {
                    findobj.FinishAt = DateTime.Now;
                }
                await _orderReponsitory.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
