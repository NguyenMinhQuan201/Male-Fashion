using DataDemo.Common;
using Domain.Common;
using Domain.Common.FileStorage;
using Domain.Features;
using Domain.Models.Dto.Blog;
using Infrastructure.Reponsitories.BlogRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IStorageService _storageService;
        public BlogService(IBlogRepository blogRepository, IStorageService storageService)
        {
            _blogRepository = blogRepository;
            _storageService = storageService;
        }
        public async Task<ApiResult<bool>> Create(BlogRequestDto request)
        {
            Expression<Func<Infrastructure.Entities.Blog, bool>> expression = x => x.Name == request.Name;
            var check = await _blogRepository.FindByName(expression);
            if (check == null)
            {
                var product = new Infrastructure.Entities.Blog()
                {
                    Name = request.Name,
                    Description = request.Description,
                    SubTitle = request.SubTitle,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null,
                    Title = request.Title,
                    Image = await this.SaveFile(request.Image),
                    Status=true,
                };
                await _blogRepository.CreateAsync(product);
                return new ApiSuccessResult<bool>(true);
            }
            else
            {
                return new ApiErrorResult<bool>("Blog name has existed");
            }
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id != null)
            {
                var findobj = await _blogRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = false;
                await _blogRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<BlogVm>>> GetAll(int? pageSize, int? pageIndex, string?search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Blog, bool>> expression1 = x => x.Status == true;
            var totalRow = await _blogRepository.CountAsync(expression1);
            var query = await _blogRepository.GetAll(pageSize, pageIndex, expression1);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Blog, bool>> expression2 = x => x.Name.Contains(search) && x.Status == true;
                query = await _blogRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _blogRepository.CountAsync(expression2);
            }
            var data = query
                .Select(x => new BlogVm()
                {
                    Image = x.Image,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Title = x.Title,
                    IdBlog = x.IdBlog,
                    Name = x.Name,
                }).ToList();
            var pagedResult = new PagedResult<BlogVm>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<BlogVm>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<BlogVm>>(pagedResult);
        }

        public async Task<IEnumerable<BlogVm>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<BlogVm>> GetById(int id)
        {
            if (id!=null)
            {
                var findobj = await _blogRepository.GetById(id);
                if (findobj == null)
                {
                    return null;
                }

                var obj = new BlogVm()
                {
                    Image = findobj.Image,
                    Description = findobj.Description,
                    CreatedAt = findobj.CreatedAt,
                    UpdatedAt = findobj.UpdatedAt,
                    Title = findobj.Title,
                    IdBlog = findobj.IdBlog,
                    Name = findobj.Name,
                };
                return new ApiSuccessResult<BlogVm>(obj);
            }
            return new ApiErrorResult<BlogVm>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<BlogVm>>> GetDeleted(int? pageSize, int? pageIndex, string?search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Blog, bool>> expression1 = x => x.Status == false;
            var totalRow = await _blogRepository.CountAsync(expression1);
            var query = await _blogRepository.GetAll(pageSize, pageIndex, expression1);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Blog, bool>> expression2 = x => x.Name.Contains(search) && x.Status == false;
                query = await _blogRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _blogRepository.CountAsync(expression2);
            }
            var data = query
                .Select(x => new BlogVm()
                {
                    Image = x.Image,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Title = x.Title,
                    IdBlog = x.IdBlog,
                    Name = x.Name,
                }).ToList();
            var pagedResult = new PagedResult<BlogVm>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = false
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<BlogVm>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<BlogVm>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Restore(int id)
        {
            if (id != null)
            {
                var findobj = await _blogRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = true;
                await _blogRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, BlogRequestEditDto request)
        {
            if (id != null)
            {
                var findobj = await _blogRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Name = request.Name;
                findobj.Title = request.Title;
                findobj.UpdatedAt = DateTime.Now;
                findobj.Description = request.Description;
                await _blogRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
