using Domain.Features.Discount;
using Domain.Models.Dto.Discount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpPost("create-discount")]
        public async Task<IActionResult> Create([FromBody] DiscountCreateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
                return BadRequest(result);
            }
        }
        [HttpPut("update-discount")]
        public async Task<IActionResult> Update(int id, [FromBody] DiscountUpdateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.Update(id, request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
                return BadRequest(result);
            }
        }
        [HttpDelete("delete-discount")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
                return BadRequest(result);
            }
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.GetById(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
                return BadRequest(result);
            }
        }
        [HttpGet("get-all-discount")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string ?name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.GetAll(pageSize,pageIndex,name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
                return BadRequest(result);
            }
        }
        [HttpGet("get-deleted-discounts")]
        public async Task<IActionResult> GetDeleted(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.GetDeletedDiscount(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }

            return BadRequest();
        }
        [HttpPut("restore")]
        public async Task<IActionResult> Restore(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _discountService.Restore(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }
    }
}
