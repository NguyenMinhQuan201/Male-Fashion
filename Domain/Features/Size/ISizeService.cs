using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Size
{
    public interface ISizeService
    {
        public Task<ApiResult<bool>> Create(SizeRequestDto request);
        public Task<ApiResult<bool>> Update(int id, SizeRequestDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<SizeRequestDto>> GetById(int id);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<PagedResult<SizeRequestDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<PagedResult<SizeRequestDto>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search);
    }
}
