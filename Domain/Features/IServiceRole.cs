using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.RoleDto;

namespace Domain.Features
{
    public interface IServiceRole
    {
        public Task<bool> Register(RoleRequestDto request);
        public Task<bool> Edit(RoleRequestDto request);
        public Task<bool> Remove(string id);
        public Task<RoleVmDto> GetById(string id);
        public Task<ApiResult<PagedResult<RoleVmDto>>> GetAllRole(int? pageSize, int? pageIndex, string? search);

    }
}
