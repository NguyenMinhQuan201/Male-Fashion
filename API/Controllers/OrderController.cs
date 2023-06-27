using Domain.Features;
using Domain.Models.Dto.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
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
            if (result.IsSuccessed==true) return BadRequest();
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
        [HttpGet("chart-col")]
        public async Task<IActionResult> GetCol()
        {
            return Ok(await _orderService.GetAllByMonth());
        }
        [HttpGet("chart-rad")]
        public async Task<IActionResult> GetRad()
        {
            return Ok(await _orderService.GetAllByYear());
        }
    }
}
