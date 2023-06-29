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

namespace Infrastructure.Reponsitories.OperationReponsitories
{
    public class OperationReponsitories : RepositoryBase<Operation>, IOperationRepository
    {
        private readonly MaleFashionDbContext _dbcontext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _OperationManager;
        public OperationReponsitories(MaleFashionDbContext dbContext, SignInManager<AppUser> signInManager, UserManager<AppUser> OperationManager) : base(dbContext)
        {
            _dbcontext = dbContext;
            _signInManager = signInManager; 
            _OperationManager = OperationManager;
        }

        /*public Tokens GenerateRefreshToken(string OperationName)
        {
            throw new NotImplementedException();
        }

        public Tokens GenerateToken(string OperationName)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }*/
    }
}
