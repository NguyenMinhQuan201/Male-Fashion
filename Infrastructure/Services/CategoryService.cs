using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Category;
using Infrastructure.Reponsitories.CategoryReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Features.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryReponsitories;
        public CategoryService(ICategoryRepository categoryReponsitories)
        {
            _categoryReponsitories = categoryReponsitories;
        }
        public async Task<ApiResult<bool>> Create(CategoryRequestDto request)
        {
            var obj = new Infrastructure.Entities.Category()
            {
                CreatedAt = DateTime.Now,
                Name = request.Name,
                Icon= request.Icon,
                UpdatedAt = DateTime.Now,
            };
            await _categoryReponsitories.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id == null)
            {
                var findobj = await _categoryReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Category()
                {
                    IdCategory = id,
                    CreatedAt = findobj.CreatedAt,
                    Name = findobj.Name,
                    Icon = findobj.Icon,
                    UpdatedAt = findobj.UpdatedAt,

                };
                await _categoryReponsitories.DeleteAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<CategoryRequestDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _categoryReponsitories.CountAsync();
            var query = await _categoryReponsitories.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Category, bool>> expression = x => x.Name.Contains(search);
                query = await _categoryReponsitories.GetAll(pageSize, pageIndex, expression);
                totalRow = await _categoryReponsitories.CountAsync(expression);
            }
            //Paging
            /*totalRow = query.Result.Count();*/
            var data = query
                .Select(x => new CategoryRequestDto()
                {
                    IdCategory = x.IdCategory,
                    Name=x.Name,
                    CreatedAt = x.CreatedAt,
                    Icon = x.Icon,
                    UpdatedAt = x.UpdatedAt,

                }).ToList();
            var pagedResult = new PagedResult<CategoryRequestDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<CategoryRequestDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<CategoryRequestDto>>(pagedResult);
        }

        public async Task<IEnumerable<CategoryRequestDto>> GetAll()
        {
            var result = await _categoryReponsitories.GetAll();
            return result.Select(x => new CategoryRequestDto()
            {
                IdCategory = x.IdCategory,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                Icon = x.Icon,
                UpdatedAt = x.UpdatedAt,

            }).ToList(); 
        }

        public async Task<ApiResult<CategoryRequestDto>> GetById(int id)
        {
            if (id != null)
            {
                var findobj = await _categoryReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<CategoryRequestDto>("Không tìm thấy đối tượng");
                }
                var obj = new CategoryRequestDto()
                {
                    IdCategory = findobj.IdCategory,
                    Name = findobj.Name,
                    CreatedAt = findobj.CreatedAt,
                    Icon = findobj.Icon,
                    UpdatedAt = findobj.UpdatedAt,

                };
                return new ApiSuccessResult<CategoryRequestDto>(obj);
            }
            return new ApiErrorResult<CategoryRequestDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<ApiResult<PagedResult<CategoryRequestDto>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, CategoryRequestDto request)
        {
            if (id == null)
            {
                var findobj = await _categoryReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Category()
                {
                    IdCategory = findobj.IdCategory,
                    Name = findobj.Name,
                    CreatedAt = findobj.CreatedAt,
                    Icon = findobj.Icon,
                    UpdatedAt = findobj.UpdatedAt,

                };
                await _categoryReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
