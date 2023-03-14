using Domain.Models.Dto.RoleDto;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.Role
{
    public class ServiceRole : IServiceRole
    {
        private readonly RoleManager<AppRole> _roleManager;
        public ServiceRole(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<RoleVmDto>> GetAllRole()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVmDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }
        public async Task<bool> Register(RoleRequestDto request)
        {
            var name = _roleManager.FindByNameAsync(request.Name);
            if (name.Result != null)
            {
                return false;
            }
            var user = new AppRole()
            {
                Description = request.Name,
                Name = request.Name,
                NormalizedName = request.Name
            };
            var end = await _roleManager.CreateAsync(user);
            if (end.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Remove(RemoveRoleRequestDto request)
        {
            var val = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (val == null)
            {
                return false;
            }
            var end = await _roleManager.DeleteAsync(val);
            if (end.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
