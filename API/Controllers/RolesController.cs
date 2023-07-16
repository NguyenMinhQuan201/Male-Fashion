using Domain.Features;
using Domain.Models.Dto.RoleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IServiceRole _serviceRole;

        public RolesController( IServiceRole ServiceRole)
        {
            _serviceRole = ServiceRole;
        }
        [HttpGet("getroles")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            var roles = await _serviceRole.GetAllRole(pageSize, pageIndex, search);
            return Ok(roles);
        }
        [HttpPost("add-role")]
        public async Task<IActionResult> CreaterRole(RoleRequestDto request)
        {
            var result = await _serviceRole.Register(request);
            return Ok();
        }
        [HttpPut("edit-role")]
        public async Task<IActionResult> EditRole(RoleRequestDto request)
        {
            var result = await _serviceRole.Edit(request);
            return Ok();
        }
        [HttpDelete("delete-role")]
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _serviceRole.Remove(id);
            return Ok();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _serviceRole.GetById(id);
            return Ok(result);
        }
    }
}
