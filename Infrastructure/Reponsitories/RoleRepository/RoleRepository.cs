using Domain.Models.Dto.RoleDto;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using Infrastructure.Reponsitories.UserReponsitories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.RoleReponsitories
{
    public class RoleReponsitories : RepositoryBase<AppRole>, IRoleRepository
    {
        private readonly MaleFashionDbContext _dbcontext;
        private readonly SignInManager<AppRole> _signInManager;
        private readonly RoleManager<AppRole> _RoleManager;
        public RoleReponsitories(MaleFashionDbContext dbContext, SignInManager<AppRole> signInManager, RoleManager<AppRole> RoleManager) : base(dbContext)
        {
            _dbcontext = dbContext;
            _signInManager = signInManager; 
            _RoleManager = RoleManager;
        }
    }
}
