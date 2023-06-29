using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IModuleService
    {
        public Task<ModuleDto> Create(ModuleDto request);
        public Task<ModuleDto> Update(int id, ModuleDto request);
        public Task<bool> Delete(int id);
        public Task<ModuleDto> GetById(int id);
        public Task<ApiResult<PagedResult<ModuleDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<PagedResult<ModuleDto>>> GetDeletedModule(int? pageSize, int? pageIndex, string search);

    }
}
