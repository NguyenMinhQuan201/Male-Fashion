using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Color;
using Infrastructure.Reponsitories.ColorReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Color
{
    public class ColorService : IColorService
    {
        private readonly IColorReponsitories _colorReponsitories;
        public ColorService(IColorReponsitories colorReponsitories)
        {
            _colorReponsitories = colorReponsitories;
        }
        public async Task<ApiResult<bool>> Create(ColorRequestDto request)
        {
            if (!String.IsNullOrEmpty(request.ColorName))
            {
                var obj = new Infrastructure.Entities.Color()
                {

                    NameColor = request.ColorName,

                };
                await _colorReponsitories.CreateAsync(obj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id == null)
            {
                var findobj = await _colorReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Color()
                {
                    Id = id,
                    NameColor = findobj.NameColor,

                };
                await _colorReponsitories.DeleteAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<ColorRequestDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _colorReponsitories.CountAsync();
            var query = await _colorReponsitories.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Color, bool>> expression = x => x.NameColor.Contains(search);
                query = await _colorReponsitories.GetAll(pageSize, pageIndex, expression);
                totalRow = await _colorReponsitories.CountAsync(expression);
            }
            //Paging
            /*totalRow = query.Result.Count();*/
            var data = query
                .Select(x => new ColorRequestDto()
                {
                    Id = x.Id,
                    ColorName = x.NameColor,
                }).ToList();
            var pagedResult = new PagedResult<ColorRequestDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ColorRequestDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ColorRequestDto>>(pagedResult);
        }

        public async Task<ApiResult<ColorRequestDto>> GetById(int id)
        {
            if (id == null)
            {
                var findobj = await _colorReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<ColorRequestDto>("Không tìm thấy đối tượng");
                }
                var obj = new ColorRequestDto()
                {
                    Id = findobj.Id,
                    ColorName = findobj.NameColor,

                };
                return new ApiSuccessResult<ColorRequestDto>(obj);
            }
            return new ApiErrorResult<ColorRequestDto>("Lỗi tham số chuyền về null hoặc trống");
        }
        public Task<ApiResult<PagedResult<ColorRequestDto>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, ColorRequestDto request)
        {
            if (id == null)
            {
                var findobj = await _colorReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Color()
                {
                    Id = request.Id,
                    NameColor = request.ColorName,

                };
                await _colorReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
