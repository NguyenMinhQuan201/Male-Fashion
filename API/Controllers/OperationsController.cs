using Domain.Features;
using Domain.Models.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /* [Authorize]*/
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _OperationService;
        public OperationsController(IOperationService OperationService)
        {
            _OperationService = OperationService;
        }
        [HttpPost("add-Operation")]
        public async Task<IActionResult> Create([FromBody] OperationDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _OperationService.Create(request);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] OperationDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _OperationService.Update(id, request);
                if (result != null)
                {
                    return Ok(result);
                }

            }
            return BadRequest();
        }
        /*[HttpPut("restore")]
        public async Task<IActionResult> Restore(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _OperationService.Restore(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }*/
        [HttpDelete("delete-Operation")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _OperationService.Delete(id);
                return Ok(result);
            }
        }
        [HttpGet("get-by-name-Operation")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _OperationService.GetAll(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }

            return BadRequest();
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
                var result = await _OperationService.GetById(id);
                if (result!=null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-deleted-Operations")]
        public async Task<IActionResult> GetDeletedOperation(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _OperationService.GetDeletedOperation(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }

            return BadRequest();
        }
    }
}
