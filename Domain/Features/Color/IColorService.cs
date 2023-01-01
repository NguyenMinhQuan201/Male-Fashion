using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Color
{
    public interface IColorService
    {
        public Task<ApiResult<bool>> Create(ColorRequestDto request);
        public Task<ApiResult<bool>> Update(int id, ColorRequestDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<ColorRequestDto>> GetById(int id);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<PagedResult<ColorRequestDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<PagedResult<ColorRequestDto>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search);
    }
}
