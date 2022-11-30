using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Discount;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Discount
{
    public class DiscountService : IDiscountService
    {
        private readonly MaleFashionDbContext _dbContext;
        public DiscountService(MaleFashionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResult<bool>> Create(DiscountCreateRequestDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Doi tuong khong ton tai");
            }
            var obj = new Infrastructure.Entities.Promotion()
            {
                Name = request.Name,
                CreatedAt = DateTime.Now,
                FromDate = request.FromDate,
                Percent = request.Percent,
                UpdatedAt = DateTime.Now,
                ToDate = request.ToDate,
                IsEnable=true
            };
            await _dbContext.Promotions.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return new ApiErrorResult<bool>("Khong co Id");
            }
            var findObj = await _dbContext.Promotions.FindAsync(id);
            if (findObj == null)
            {
                return new ApiErrorResult<bool>("Khong co doi tuong");
            }
            findObj.IsEnable = false;
            _dbContext.Promotions.Update(findObj);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<GetAllDiscount>>> GetAll(int? pageSize, int? pageIndex, string search)
        {

            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var query = from s in _dbContext.Promotions
                        
                        select s;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value).Select(x => new GetAllDiscount()
                {
                    IdPromotion = x.IdPromotion,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy"),
                    FromDate = x.FromDate.ToString("MM/dd/yyyy"),
                    Percent = x.Percent,
                    UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy"),
                    ToDate = x.ToDate.ToString("MM/dd/yyyy"),
                    IsEnable = x.IsEnable,
                }).ToListAsync();
            var pagedResult = new PagedResult<GetAllDiscount>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetAllDiscount>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetAllDiscount>>(pagedResult);
        }

        public async Task<ApiResult<GetDiscount>> GetById(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return new ApiErrorResult<GetDiscount>("Khong co Id");
            }
            var findObj = await _dbContext.Promotions.FindAsync(id);
            if (findObj == null)
            {
                return new ApiErrorResult<GetDiscount>("Khong co doi tuong");
            }
            var result = new GetDiscount()
            {
                IdPromotion = findObj.IdPromotion,
                Name = findObj.Name,
                CreatedAt = findObj.CreatedAt.ToString("MM/dd/yyyy"),
                FromDate = findObj.FromDate.ToString("MM/dd/yyyy"),
                Percent = findObj.Percent,
                UpdatedAt = findObj.UpdatedAt.ToString("MM/dd/yyyy"),
                ToDate = findObj.ToDate.ToString("MM/dd/yyyy"),
            };
            return new ApiSuccessResult<GetDiscount>(result);
        }

        public async Task<ApiResult<PagedResult<GetAllDiscount>>> GetDeletedDiscount(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var query = from s in _dbContext.Promotions
                        where s.IsEnable == false
                        select s;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value).Select(x => new GetAllDiscount()
                {
                    IdPromotion = x.IdPromotion,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy"),
                    FromDate = x.FromDate.ToString("MM/dd/yyyy"),
                    Percent = x.Percent,
                    UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy"),
                    ToDate = x.ToDate.ToString("MM/dd/yyyy"),
                }).ToListAsync();
            var pagedResult = new PagedResult<GetAllDiscount>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetAllDiscount>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetAllDiscount>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Restore(int id)
        {
            if (id.ToString() == "")
            {
                return new ApiErrorResult<bool>("Khong co Id");
            }
            var findObj = await _dbContext.Promotions.FindAsync(id);
            if (findObj == null)
            {
                return new ApiErrorResult<bool>("Khong co doi tuong");
            }
            findObj.IsEnable = true;
            _dbContext.Promotions.Update(findObj);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(int id, DiscountUpdateRequestDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Khong doi tuong");
            }
            var findById = await _dbContext.Promotions.FindAsync(id);
            if (findById == null)
            {
                return new ApiErrorResult<bool>("Khong co doi tuong");
            }
            findById.Name = request.Name;
            findById.Percent = request.Percent;
            
            findById.UpdatedAt = DateTime.Now;
            findById.ToDate = request.ToDate;
            findById.FromDate = request.FromDate;

            _dbContext.Promotions.Update(findById);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
