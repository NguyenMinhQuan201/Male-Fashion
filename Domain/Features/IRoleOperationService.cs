using Domain.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IRoleOperationService
    {
        public Task<RoleOperationDto> Create(RoleOperationDto request);
        public Task<RoleOperationDto> Update(int id, RoleOperationDto request);
        public Task<bool> Delete(int id);
        public Task<int> GetById(int id);
    }
}
