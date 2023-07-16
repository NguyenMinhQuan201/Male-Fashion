using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.RoleDto;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Features.Role
{
    public class ServiceRole : IServiceRole
    {
        private readonly RoleManager<AppRole> _roleManager;
        public ServiceRole(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> Edit(RoleRequestDto request)
        {
            var val = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (val.Name == null)
            {
                return false;
            }
            val.Description = request.Description;
            val.Name = request.Name;
            var end = await _roleManager.UpdateAsync(val);
            if (end.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<ApiResult<PagedResult<RoleVmDto>>> GetAllRole(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize == null)
            {
                pageSize = 10;
            }
            if (pageIndex == null)
            {
                pageIndex = 1;
            }
            var totalRow = _roleManager.Roles.Count();
            var query = (List<AppRole>)_roleManager.Roles.Skip((int)((pageIndex - 1) * pageSize)).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                query = (List<AppRole>)_roleManager.Roles.Skip((int)((pageIndex - 1) * pageSize)).Where(x=>x.Name.Contains(search)).ToList();
                totalRow = _roleManager.Roles.Where(x => x.Name.Contains(search)).Count();
            }
            var roles = query
                .Select(x => new RoleVmDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();

            var pagedResult = new PagedResult<RoleVmDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = roles,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<RoleVmDto>>("Khong co gi ca");
            }

            return new ApiSuccessResult<PagedResult<RoleVmDto>>(pagedResult);
        }

        public async Task<RoleVmDto> GetById(string id)
        {
            var val = await _roleManager.FindByIdAsync(id);
            if (val.Name != null)
            {
                var role = new RoleVmDto()
                {
                    Description = val.Description,
                    Name = val.Name,
                    Id = val.Id
                };
                return role;
            }
            throw new NotImplementedException();
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

        public async Task<bool> Remove(string id)
        {
            var val = await _roleManager.FindByIdAsync(id);
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
