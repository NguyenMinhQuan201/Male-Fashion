using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Notifi;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.OrderDetailReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using Infrastructure.Reponsitories.ProductReponsitories;
using Microsoft.AspNetCore.SignalR;
using System.Linq.Expressions;

namespace Domain.Features.Order
{
    public class OrderService : IOrderService
    {
        private readonly IHubContext<NotiHub> _hubContext;
        private readonly IOrderDetailRepository _orderDetailReponsitory;
        private readonly IOrderRepository _orderReponsitory;
        private readonly IProductRepository _productReponsitory;
        private readonly IMapper _mapper;
        public OrderService(IHubContext<NotiHub> hubContext, IOrderRepository orderReponsitory, IOrderDetailRepository orderDetailReponsitory, IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderDetailReponsitory = orderDetailReponsitory;
            _orderReponsitory = orderReponsitory;
            _productReponsitory = productRepository;
            _hubContext= hubContext;
        }
        public void SendNotification()
        {
            _hubContext.Clients.All.SendAsync("ReceiveNotification", "Hello from ASP.NET Core!");
        }
        public async Task<ApiResult<OrderDto>> Create(OrderDto request)
        {
            var order = new Infrastructure.Entities.Order()
            {
                Status = request.Status,
                SumPrice = request.SumPrice,
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
                        IdProduct = orderDetail.IdProduct,
                        CreatedAt = DateTime.Now,
                        DeliveryAt = DateTime.Now,
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
                var result = await _orderReponsitory.CreateAsyncFLByOrder(order);
                request.Id = request.Id;
                SendNotification();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDto>(ex.ToString());
            }
            return new ApiSuccessResult<OrderDto>(request);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id != null)
            {
                var findobj = await _orderReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = 0;
                await _orderReponsitory.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
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
            Expression<Func<Infrastructure.Entities.Order, bool>> expression = x => x.Status != 0;
            var totalRow = await _orderReponsitory.CountAsync(expression);
            var query = await _orderReponsitory.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Order, bool>> expression2 = x => x.NameCustomer.Contains(search) && x.Status != 0;
                query = await _orderReponsitory.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _orderReponsitory.CountAsync(expression2);
            }
            //Paging
            /*totalRow = query.Result.Count();*/
            var data = query
                .Select(x => new GetOrderDto()
                {
                    IdOrder = x.IdOrder,
                    Status = x.Status,
                    SumPrice = x.SumPrice,
                    NameCustomer = x.NameCustomer,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    CreatedAt = x.CreatedAt,
                    DeliveryAt = x.DeliveryAt,
                    FinishAt = x.FinishAt,
                    Note = x.Note,
                    Payments = x.Payments,
                }).ToList();
            var pagedResult = new PagedResult<GetOrderDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetOrderDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetOrderDto>>(pagedResult);
        }



        public async Task<ApiResult<List<OrderDetailDto>>> GetAllOrderDetail(int id)
        {
            Expression<Func<Infrastructure.Entities.OrderDetails, bool>> expression = x => x.IdOrder == id;
            var query = await _orderDetailReponsitory.GetByCondition(expression);
            var queryProduct = await _productReponsitory.GetAll();
            /*var data = query
                .Select(x => new OrderDetailDto()
                {
                    IdOrder=x.IdOrder,
                    IdProduct=x.IdProduct,
                    Discounnt=x.Discounnt,
                    Quantity=x.Quantity,
                    Price=x.Price,
                }).ToList();*/
            var data = (from orderDetailtbl in query
                        join producttbl in queryProduct on orderDetailtbl.IdProduct equals producttbl.IdProduct
                        select new OrderDetailDto()
                        {
                            IdOrder = orderDetailtbl.IdOrder,
                            IdProduct = orderDetailtbl.IdProduct,
                            Discounnt = orderDetailtbl.Discounnt,
                            Quantity = orderDetailtbl.Quantity,
                            Price = orderDetailtbl.Price,
                            NameProduct = producttbl.Name,
                        }).ToList();
            var temp = new List<OrderDetailDto>();
            if (data == null)
            {
                return new ApiErrorResult<List<OrderDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<List<OrderDetailDto>>(data);
        }

        public async Task<ApiResult<PagedResult<GetOrderDto>>> GetAllPagingRemoved(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Order, bool>> expression = x => x.Status == 0;
            var totalRow = await _orderReponsitory.CountAsync(expression);
            var query = await _orderReponsitory.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Order, bool>> expression2 = x => x.NameCustomer.Contains(search) && x.Status == 0;
                query = await _orderReponsitory.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _orderReponsitory.CountAsync(expression2);
            }
            //Paging
            var data = query
                .Select(x => new GetOrderDto()
                {
                    IdOrder = x.IdOrder,
                    Status = x.Status,
                    SumPrice = x.SumPrice,
                    NameCustomer = x.NameCustomer,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    CreatedAt = x.CreatedAt,
                    DeliveryAt = x.DeliveryAt,
                    FinishAt = x.FinishAt,
                    Note = x.Note,
                    Payments = x.Payments,
                }).ToList();
            var pagedResult = new PagedResult<GetOrderDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = false
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetOrderDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetOrderDto>>(pagedResult);
        }

        public async Task<GetOrderDto> GetById(int id)
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

        public async Task<ApiResult<bool>> Restore(int id)
        {
            if (id != null)
            {
                var findobj = await _orderReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = 1;
                await _orderReponsitory.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
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
                if (findobj.Status == 2)
                {
                    findobj.FinishAt = DateTime.Now;
                }
                await _orderReponsitory.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
        public async Task<IEnumerable<ChartCol>> GetAllByMonth()
        {
            DateTime now = DateTime.Now;
            // Lấy năm từ ngày giờ hiện tại
            int currentYear = now.Year;
            var lst = new List<ChartCol>();

            var getAll = _orderReponsitory.GetAll().Result.Where(x => x.CreatedAt.Year == currentYear).ToList();
            for (int i = 0; i < 12; i++)
            {
                var obj = new ChartCol()
                {
                    ChartPrice = getAll.Where(x => x.CreatedAt.Month == (i + 1)).Sum(x => x.SumPrice),
                    Month = i + 1
                };
                lst.Add(obj);
            }
            return lst;
        }
        public async Task<IEnumerable<ChartRadius>> GetAllByYear()
        {
            DateTime now = DateTime.Now;
            // Lấy năm từ ngày giờ hiện tại
            int currentYear = now.Year;
            var lst = new List<ChartRadius>();

            var getAllYear = _orderReponsitory.GetAll().Result.Select(x => x.CreatedAt.Year).Distinct().ToList();
            var getAll = _orderReponsitory.GetAll().Result.ToList();
            foreach (var item in getAllYear)
            {
                var obj = new ChartRadius()
                {
                    ChartPrice = getAll.Where(x => x.CreatedAt.Year == item).Sum(x => x.SumPrice),
                    Year = item
                };
                lst.Add(obj);
            }
            return lst;
        }

        public async Task<List<NotifiDto>> GetAllNotifiDto()
        {
            return _mapper.Map<List<NotifiDto>>(await _orderReponsitory.GetAllNoti());
        }
        public async Task<bool> Readed(long id)
        {
            var getNos = await _orderReponsitory.GetAllNoti();
            var find = getNos.Where(x => x.Id == id).FirstOrDefault();
            if (find != null)
            {
                find.IsRead = true;
                await _orderReponsitory.UpdateNoti(find);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
