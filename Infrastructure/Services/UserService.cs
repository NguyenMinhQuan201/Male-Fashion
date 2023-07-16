using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.RoleDto;
using Domain.Models.Dto.UserDto;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.ModuleReponsitories;
using Infrastructure.Reponsitories.OperationReponsitories;
using Infrastructure.Reponsitories.RoleOperationRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly IRoleOperationRepository _roleOperationRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IModuleRepository _moduleRepository;
        public UserService(IConfiguration configuration, 
            MaleFashionDbContext dbcontext, 
            SignInManager<AppUser> signInManager,
            IOperationRepository operationRepository,
            UserManager<AppUser> userManage,
            IRoleOperationRepository roleOperationRepository,
            IModuleRepository moduleRepository,
            RoleManager<AppRole> roleManager)
        {
            _operationRepository = operationRepository;
            _moduleRepository= moduleRepository;
            _roleOperationRepository= roleOperationRepository;
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
            int totalRow =await query.CountAsync();
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
                Roles = roles,
            };
            return Uservm;
        }

        /*public async Task<string> GetNewToken(string token)
        {
            throw new NotImplementedException();
        }*/

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
        private string GenerateRefreshToken()
        {
            var random = new Byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            };
        }
        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }
        public async Task<ApiResult<Tokens>> Login(UserLoginRequestDto request)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == request.UserName);
            if (user == null)
                return new ApiErrorResult<Tokens>("Tai khoan ko ton tai");
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<Tokens>("Đăng nhập không đúng");
            }
            var Mod = await _moduleRepository.GetAllAsQueryable();
            var RoleOpe = await _roleOperationRepository.GetAllAsQueryable();
            var Ope = await _operationRepository.GetAllAsQueryable();
            var rolesUser = await _userManager.GetRolesAsync(user);
            var query = (from Moduletbl in Mod
                         join Operationtbl in Ope on Moduletbl.Id equals Operationtbl.ModuleId
                         join RoleOperationtbl in RoleOpe on Operationtbl.Id equals RoleOperationtbl.OperationId
                         select new RoleAndOperation()
                         {
                             Code = Operationtbl.Code,
                             RoleId = Guid.Parse(RoleOperationtbl.RoleId),
                         }).ToList();
            var roles = _roleManager.Roles.Where(x=> rolesUser.Contains(x.Name)).ToList();

            var listOpe = new List<string>();
            foreach(var item in roles)
            {
                foreach (var RoleAndOperation in query)
                {
                    if(RoleAndOperation.RoleId == item.Id)
                    {
                        listOpe.Add(RoleAndOperation.Code);
                    }
                }
            }
            listOpe = listOpe.Distinct().ToList();
            
            var claims= new List<Claim>();
            /*{
                new Claim("email", user.Email);
                new Claim("firstName", user.FirstName);
                new Claim(ClaimTypes.Role, string.Join(";", rolesUser));
                //new Claim("policy", string.Join(";",listOpe)),
                new Claim("userName", request.UserName)
            };*/
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("firstName", user.FirstName));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(";", rolesUser)));
            claims.Add(new Claim("userName", request.UserName));
            foreach (var ope in listOpe)
            {
                claims.Add(new Claim(ope, ope));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            string refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            var getToken = new Tokens()
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Refresh_Token = refreshToken
            };

            return new ApiSuccessResult<Tokens>(getToken);
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
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, request.PassWord);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public string GenerateRefreshToken(string userName)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        ClaimsPrincipal IUserService.GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }

        public async Task<ApiResult<Tokens>> RenewToken(TokenRequestDto request)
        {
            var jwtTokenSecurityHandler = new JwtSecurityTokenHandler();
            var decodeToken = jwtTokenSecurityHandler.ReadJwtToken(request.Access_Token);
            var email = decodeToken.Claims.First(claim => claim.Type == "email").Value;
            var firstName = decodeToken.Claims.First(claim => claim.Type == "firstName").Value;
            var role = decodeToken.Claims.First(claim => claim.Type == "role").Value;
            var userName = decodeToken.Claims.First(claim => claim.Type == "userName").Value;
            var user = await _userManager.FindByNameAsync(userName);

           
            var claims = new[]
            {
                new Claim("email",email),
                new Claim("firstName",firstName),
                new Claim("role", string.Join(";",role)),
                new Claim("userName", userName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            var getToken = new Tokens()
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Refresh_Token = refreshToken
            };

            return new ApiSuccessResult<Tokens>(getToken);
        }

        
    }
}
