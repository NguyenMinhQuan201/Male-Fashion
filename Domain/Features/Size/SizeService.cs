using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Size;
using Infrastructure.Reponsitories.SizeReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Size
{
    public class SizeService : ISizeService
    {
        private readonly ISizeReponsitories _sizeReponsitories;
        public SizeService(ISizeReponsitories sizeReponsitories)
        {
            _sizeReponsitories = sizeReponsitories;
        }
        public async Task<ApiResult<bool>> Create(SizeRequestDto request)
        {
            if (!String.IsNullOrEmpty(request.SizeName))
            {
                var obj = new Infrastructure.Entities.Size()
                {

                    NameSize = request.SizeName,

                };
                await _sizeReponsitories.CreateAsync(obj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id == null)
            {
                var findobj = await _sizeReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Size()
                {
                    Id = id,
                    NameSize = findobj.NameSize,

                };
                await _sizeReponsitories.DeleteAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<SizeRequestDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _sizeReponsitories.CountAsync();
            var query = await _sizeReponsitories.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Size, bool>> expression = x => x.NameSize.Contains(search);
                query = await _sizeReponsitories.GetAll(pageSize, pageIndex, expression);
                totalRow = await _sizeReponsitories.CountAsync(expression);
            }
            //Paging
            /*totalRow = query.Result.Count();*/
            var data = query
                .Select(x => new SizeRequestDto()
                {
                    Id = x.Id,
                    SizeName = x.NameSize,
                }).ToList();
            var pagedResult = new PagedResult<SizeRequestDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<SizeRequestDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<SizeRequestDto>>(pagedResult);
        }

        public async Task<ApiResult<SizeRequestDto>> GetById(int id)
        {
            if (id == null)
            {
                var findobj = await _sizeReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<SizeRequestDto>("Không tìm thấy đối tượng");
                }
                var obj = new SizeRequestDto()
                {
                    Id = findobj.Id,
                    SizeName = findobj.NameSize,

                };
                return new ApiSuccessResult<SizeRequestDto>(obj);
            }
            return new ApiErrorResult<SizeRequestDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<ApiResult<PagedResult<SizeRequestDto>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, SizeRequestDto request)
        {
            if (id == null)
            {
                var findobj = await _sizeReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Size()
                {
                    Id = request.Id,
                    NameSize = request.SizeName,

                };
                await _sizeReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
