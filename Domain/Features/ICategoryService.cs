using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface ICategoryService
    {
        public Task<ApiResult<bool>> Create(CategoryRequestDto request);
        public Task<ApiResult<bool>> Update(int id, CategoryRequestDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<CategoryRequestDto>> GetById(int id);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<PagedResult<CategoryRequestDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<IEnumerable<CategoryRequestDto>> GetAll();
        public Task<ApiResult<PagedResult<CategoryRequestDto>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search);
    }
}
