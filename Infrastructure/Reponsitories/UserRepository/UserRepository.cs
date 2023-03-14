using Domain.Models.Dto.UserDto;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.UserReponsitories
{
    public class UserReponsitories : RepositoryBase<AppUser>, IUserRepository
    {
        private readonly MaleFashionDbContext _dbcontext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public UserReponsitories(MaleFashionDbContext dbContext, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : base(dbContext)
        {
            _dbcontext = dbContext;
            _signInManager = signInManager; 
            _userManager = userManager;
        }

        /*public Tokens GenerateRefreshToken(string userName)
        {
            throw new NotImplementedException();
        }

        public Tokens GenerateToken(string userName)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }*/
    }
}
