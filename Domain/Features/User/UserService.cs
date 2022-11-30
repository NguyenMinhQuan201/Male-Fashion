using DataDemo.Common;
using Domain.Common;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.RoleDto;
using Domain.Models.Dto.UserDto;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.IServices.User
{
    public class UserService : IUserService
    {
        private readonly MaleFashionDbContext _dbcontext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration, MaleFashionDbContext dbcontext, SignInManager<AppUser> signInManager, UserManager<AppUser> userManage, RoleManager<AppRole> roleManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManage;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<bool> Create(UserCreateRequestDto request)
        {
            var Email = _userManager.Users.FirstOrDefault(x => x.Email == request.Email);
            if (Email != null)
            {
                return false;
            }
            var UserName = await _userManager.FindByNameAsync(request.UserName);
            if (UserName != null)
            {
                return false;
            }
            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.PassWord);
            //Confirm Gmail
            /*var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var end = await _userManager.ConfirmEmailAsync(user, token);*/
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<ApiResult<PagedResult<UserVmDto>>> GetAll(int? pageSize, int? pageIndex, string? name)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var query = from u in _dbcontext.Users
                        select u;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.UserName.Contains(name));
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(x => new UserVmDto()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                }).ToListAsync();
            var pagedResult = new PagedResult<UserVmDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<UserVmDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<UserVmDto>>(pagedResult);
           
        }

        public async Task<UserVmDto> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                var UserNull = new UserVmDto();
                return UserNull;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var Uservm = new UserVmDto()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Id = id,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles
            };
            return Uservm;
        }

        public AuthenticationProperties GetProperties(string redirectUrl)
        {
            var result = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return result;
        }

        public async Task<string> GetTokenByLoginGG()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return "Khong co thong tin nguoi dung";

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo =
            {
                info.Principal.FindFirst(ClaimTypes.Name).Value,
                info.Principal.FindFirst(ClaimTypes.Email).Value
            };
            if (result.Succeeded)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        /*new Claim(ClaimTypes.Role, string.Join(";",roles)),*/
                        new Claim("Name",info.Principal.FindFirst(ClaimTypes.Name).Value),
                        new Claim("Email",info.Principal.FindFirst(ClaimTypes.Email).Value),

                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                AppUser user = new AppUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    FirstName = info.Principal.FindFirst(ClaimTypes.Name).Value,
                    LastName = info.Principal.FindFirst(ClaimTypes.Name).Value,

                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return "Thanh Cong";
                    }
                }
                return "Dang Nhap khong thanh cong";
            }
        }

        public async Task<string> Login(UserLoginRequestDto request)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == request.UserName);
            if (user == null)
                return ("Tai khoan ko ton tai");
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded)
            {
                return ("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return (new JwtSecurityTokenHandler().WriteToken(token));
        }
        public async Task<bool> RoleAssign(Guid id, RoleAssignRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return true;
        }

        public async Task<bool> Update(Guid id, UserUpdateRequestDto request)
        {
            var Email = _userManager.Users.Any(x => x.Email == request.Email && x.Id != id);
            if (Email == true)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(id.ToString());

            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

    }
}
