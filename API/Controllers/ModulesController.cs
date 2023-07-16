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
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _ModuleService;
        public ModulesController(IModuleService ModuleService)
        {
            _ModuleService = ModuleService;
        }
        [HttpPost("add-Module")]
        public async Task<IActionResult> Create([FromBody] ModuleDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _ModuleService.Create(request);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] ModuleDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _ModuleService.Update(id, request);
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
                var result = await _ModuleService.Restore(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }*/
        [HttpDelete("delete-Module")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _ModuleService.Delete(id);
                return Ok(result);
            }
        }
        [HttpGet("get-by-name-Module")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _ModuleService.GetAll(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result);
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
                var result = await _ModuleService.GetById(id);
                if (result!=null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-deleted-Modules")]
        public async Task<IActionResult> GetDeletedModule(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _ModuleService.GetDeletedModule(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }

            return BadRequest();
        }
    }
}
