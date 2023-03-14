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
        public async Task<IActionResult> GetAll()
        {
            var roles = await _serviceRole.GetAllRole();
            return Ok(roles);
        }
        [HttpPost("add-role")]
        public async Task<IActionResult> CreaterRole(RoleRequestDto request)
        {
            var result = await _serviceRole.Register(request);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(RemoveRoleRequestDto request)
        {
            var result = await _serviceRole.Remove(request);
            return Ok();
        }
    }
}
