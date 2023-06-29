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
    public interface IOperationService
    {
        public Task<OperationDto> Create(OperationDto request);
        public Task<OperationDto> Update(int id, OperationDto request);
        public Task<bool> Delete(int id);
        public Task<OperationDto> GetById(int id);
        public Task<ApiResult<PagedResult<OperationDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<PagedResult<OperationDto>>> GetDeletedOperation(int? pageSize, int? pageIndex, string search);

    }
}
