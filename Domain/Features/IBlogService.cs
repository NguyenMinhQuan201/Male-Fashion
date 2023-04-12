using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IBlogService
    {
        public Task<ApiResult<bool>> Create(BlogRequestDto request);
        public Task<ApiResult<bool>> Update(int id, BlogRequestEditDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<BlogVm>> GetById(int id);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<PagedResult<BlogVm>>> GetAll(int? pageSize, int? pageIndex, string?search);
        public Task<IEnumerable<BlogVm>> GetAll();
        public Task<ApiResult<PagedResult<BlogVm>>> GetDeleted(int? pageSize, int? pageIndex, string?search);
    }
}
