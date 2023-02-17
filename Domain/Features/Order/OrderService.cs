using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.OrderDetailReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDetailReponsitory _orderDetailReponsitory;
        private readonly IOrderReponsitory _orderReponsitory;
        public OrderService(IOrderReponsitory orderReponsitory, IOrderDetailReponsitory orderDetailReponsitory)
        {
            _orderDetailReponsitory = orderDetailReponsitory;
            _orderReponsitory = orderReponsitory;
        }
        public async Task<ApiResult<bool>> Create(OrderDto request)
        {
            var order = new Infrastructure.Entities.Order()
            {
                Status = "false",
                SumPrice = 0,
                Address = request.Address,
                CreatedAt = DateTime.Now,
                DeliveryAt = DateTime.Now,
                Email = request.Email,
                /*FinishAt = null,*/
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

        public Task<ApiResult<GetOrderDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Update(int id, OrderDto request)
        {
            throw new NotImplementedException();
        }
    }
}
