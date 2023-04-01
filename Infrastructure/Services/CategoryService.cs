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
                Status = request.Status,
            };
            await _categoryReponsitories.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id != null)
            {
                var findobj = await _categoryReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = false;
                await _categoryReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
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
            Expression<Func<Infrastructure.Entities.Category, bool>> expression = x =>x.Status == true;
            var totalRow = await _categoryReponsitories.CountAsync(expression);
            var query = await _categoryReponsitories.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Category, bool>> expression2 = x => x.Name.Contains(search)&&x.Status==true;
                query = await _categoryReponsitories.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _categoryReponsitories.CountAsync(expression2);
            }
            //Paging
            var data = query
                .Select(x => new CategoryRequestDto()
                {
                    IdCategory = x.IdCategory,
                    Name=x.Name,
                    CreatedAt = x.CreatedAt,
                    Icon = x.Icon,
                    UpdatedAt = x.UpdatedAt,
                    Status = x.Status,

                }).ToList();
            var pagedResult = new PagedResult<CategoryRequestDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status=true
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
                    Status = findobj.Status,

                };
                return new ApiSuccessResult<CategoryRequestDto>(obj);
            }
            return new ApiErrorResult<CategoryRequestDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<CategoryRequestDto>>> GetDeletedCategories(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Category, bool>> expression = x => x.Status == false;
            var totalRow = await _categoryReponsitories.CountAsync(expression);
            var query = await _categoryReponsitories.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Category, bool>> expression2 = x => x.Name.Contains(search) && x.Status == false;
                query = await _categoryReponsitories.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _categoryReponsitories.CountAsync(expression2);
            }
            //Paging
            var data = query
                .Select(x => new CategoryRequestDto()
                {
                    IdCategory = x.IdCategory,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    Icon = x.Icon,
                    UpdatedAt = x.UpdatedAt,
                    Status = x.Status,

                }).ToList();
            var pagedResult = new PagedResult<CategoryRequestDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = false
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<CategoryRequestDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<CategoryRequestDto>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Restore(int id)
        {
            if (id != null)
            {
                var findobj = await _categoryReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = true;
                await _categoryReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, CategoryRequestDto request)
        {
            if (id != null)
            {
                var findobj = await _categoryReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = request.Status;
                findobj.Name = request.Name;
                findobj.Icon = request.Icon;
                findobj.UpdatedAt = DateTime.Now;
                await _categoryReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
