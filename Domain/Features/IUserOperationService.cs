using Domain.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IUserOperationService
    {
        public Task<UserOperationDto> Create(UserOperationDto request);
        public Task<UserOperationDto> Update(int id, UserOperationDto request);
        public Task<bool> Delete(int id);
        public Task<int> GetById(int id);
    }
}
