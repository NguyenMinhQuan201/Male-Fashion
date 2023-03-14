using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.RoleDto;
using Domain.Models.Dto.UserDto;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Domain.Features
{
    public interface IUserService
    {
        public Task<ApiResult<Tokens>> Login(UserLoginRequestDto request);
        public Task<ApiResult<Tokens>> RenewToken(TokenRequestDto request);
        /*public Task<string> GetNewToken(string token);*/
        public Task<bool> Create(UserCreateRequestDto request);
        public Task<bool> Update(Guid id, UserUpdateRequestDto request);
        public Task<bool> Delete(Guid id);
        public Task<ApiResult<PagedResult<UserVmDto>>> GetAll(int? pageSize, int? pageIndex, string? name);
        public Task<UserVmDto> GetById(Guid id);
        public Task<bool> RoleAssign(Guid id, RoleAssignRequestDto request);
        public AuthenticationProperties GetProperties(string redirectUrl);
        public Task<string> GetTokenByLoginGG();
        string GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
