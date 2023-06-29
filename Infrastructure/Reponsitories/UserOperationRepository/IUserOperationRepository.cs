using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Dto.UserDto;

namespace Infrastructure.Reponsitories.UserReponsitories
{
    public interface IUserOperationRepository : IRepositoryBase<UserOperation>
    {
        /*Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);*/
    }
}
