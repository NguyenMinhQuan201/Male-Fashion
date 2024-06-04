using API.Models;
using Domain.Features;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IRatingService _ratinggService;
        public OrderController(IOrderService orderService, IRatingService ratinggService)
        {
            _ratinggService = ratinggService;
            _orderService = orderService;
        }
        [HttpPost("post")]
        public async Task<IActionResult> Create([FromBody] OrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.Create(request);
            if (result.IsSuccessed == true) return BadRequest();
            return Ok(result);
        }
        [HttpPut("put")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.Update(id, request);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.Delete(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.GetById(id);
            if (result == null) return BadRequest();

            return Ok(result);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.GetAll(pageSize, pageIndex, search);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-removed")]
        public async Task<IActionResult> GetAllRemoved(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.GetAllPagingRemoved(pageSize, pageIndex, search);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-order-detail")]
        public async Task<IActionResult> GetAllOrderDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.GetAllOrderDetail(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("unremove")]
        public async Task<IActionResult> UnDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.Restore(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("chart-col-by-month")]
        public async Task<IActionResult> GetColByMonth(int month)
        {
            return Ok(await _orderService.GetAllByMonth(month));
        }

        [HttpGet("chart-col")]
        public async Task<IActionResult> GetCol(int year)
        {
            return Ok(await _orderService.GetAllByMonth(year));
        }
        [HttpGet("chart-col-month")]
        public async Task<IActionResult> GetColMonth(int year,int month)
        {
            return Ok(await _orderService.GetAllByDay(year, month));
        }
        [HttpGet("chart-rad")]
        public async Task<IActionResult> GetRad()
        {
            return Ok(await _orderService.GetAllByYear());
        }
        //public void SendNotification()
        //{
        //    _hubContext.Clients.All.SendAsync("ReceiveNotification", "Hello from ASP.NET Core!");
        //}
        [HttpGet("all-noti")]
        public async Task<IActionResult> GetAllNoti()
        {
            return Ok(await _orderService.GetAllNotifiDto());
        }
        [HttpGet("update-noti")]
        public async Task<IActionResult> GetAllNoti(long id)
        {
            return Ok(await _orderService.Readed(id));
        }
        [HttpGet("get-by-phone")]
        public async Task<IActionResult> GetOrderByPhone(int idOrder, int phone)
        {
            return Ok(await _orderService.GetAllByPhone(idOrder, phone));
        }
        [HttpGet("get-by-rating-pro")]
        public async Task<IActionResult> GetDes(int idpro)
        {
            return Ok(await _ratinggService.GetById(idpro));
        }
        [HttpPost("create-rating-pro")]
        public async Task<IActionResult> CreateRating([FromBody] RatingDto rate)
        {
            return Ok(await _ratinggService.Create(rate));
        }
        [HttpGet("get-by-date")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllDone());
        }
        [HttpGet("get-col-sale")]
        public async Task<IActionResult> ColSale()
        {
            return Ok(await _orderService.GetAllSale());
        }
    }
}
