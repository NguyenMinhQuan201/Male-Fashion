using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IDiscountService
    {
        public Task<ApiResult<bool>> Create(DiscountCreateRequestDto request);
        public Task<ApiResult<bool>> Update(int id, DiscountUpdateRequestDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<GetDiscount>> GetById(int id);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<PagedResult<GetAllDiscount>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<PagedResult<GetAllDiscount>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search);
    }
}
