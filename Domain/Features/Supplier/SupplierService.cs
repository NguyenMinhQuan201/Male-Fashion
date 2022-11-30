using DataDemo.Common;
using Domain.Common;
using Domain.Features.ManageSuppliers;
using Domain.Features.Supplier.Dto;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly MaleFashionDbContext _dbContext;
        public SupplierService(MaleFashionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResult<bool>> Create(SupplierCreateRequestDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Doi tuong khong ton tai");
            }
            var obj = new Infrastructure.Entities.Supplier()
            {

                Name = request.Name,
                Address = request.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Email = request.Email,
                Phone = request.Phone,
                IsEnable = true
            };
            await _dbContext.Suppliers.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id.ToString() == "")
            {
                return new ApiErrorResult<bool>("Khong co Id");
            }
            var findObj = await _dbContext.Suppliers.FindAsync(id);
            if (findObj == null)
            {
                return new ApiErrorResult<bool>("Khong co doi tuong");
            }
            findObj.IsEnable = false;
            _dbContext.Suppliers.Update(findObj);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<GetSupplier>>> GetAll(int? pageSize, int? pageIndex)
        {
            int P_size = 5;
            int P_pageIndexize = 1;

            if (pageSize != null)
            {
                P_size = pageSize.Value;
            }
            if (pageIndex != null)
            {
                P_pageIndexize = pageIndex.Value;
            }
            var query = await _dbContext.Suppliers.ToListAsync();
            //paging
            int totalRow = query.Count();
            var data = query.Skip((P_pageIndexize - 1) * P_size)
                .Take(P_size).Select(x => new GetSupplier()
                {
                    Id = x.IdSupplier,
                    Name = x.Name,
                    Address = x.Address,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Email = x.Email,
                    Phone = x.Phone,
                }).ToList();
            var pagedResult = new PagedResult<GetSupplier>()
            {
                TotalRecord = totalRow,
                PageSize = P_size,
                PageIndex = P_pageIndexize,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetSupplier>>("Khong co gi ca");
            }

            return new ApiSuccessResult<PagedResult<GetSupplier>>(pagedResult);
        }

        public async Task<ApiResult<GetSupplier>> GetById(int Id)
        {
            var result = await _dbContext.Suppliers.Where(x => x.IdSupplier == Id).Select(x => new GetSupplier()
            {
                Id = x.IdSupplier,
                Name = x.Name,
                Address = x.Address,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Email = x.Email,
                Phone = x.Phone,
            }).FirstOrDefaultAsync();
            if (result == null)
            {
                return new ApiErrorResult<GetSupplier>("Khong co gi ca");
            }
            return new ApiSuccessResult<GetSupplier>(result);
        }

        public async Task<ApiResult<PagedResult<GetSupplierWithConvertDate>>> GetByName(int? pageSize, int? pageIndex, string? name)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var query = from s in _dbContext.Suppliers
                        select s;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(x => new GetSupplierWithConvertDate()
                {
                    Id = x.IdSupplier,
                    Name = x.Name,
                    Address = x.Address,
                    CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy"),
                    UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy"),
                    Email = x.Email,
                    Phone = x.Phone,
                    IsEnable = x.IsEnable,
                }).ToListAsync();
            var pagedResult = new PagedResult<GetSupplierWithConvertDate>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetSupplierWithConvertDate>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetSupplierWithConvertDate>>(pagedResult);
        }


        public async Task<ApiResult<PagedResult<GetSupplierWithConvertDate>>> GetDeletedSupplier(int? pageSize, int? pageIndex, string name)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var query = from s in _dbContext.Suppliers
                        where s.IsEnable == false
                        select s;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(x => new GetSupplierWithConvertDate()
                {
                    Id = x.IdSupplier,
                    Name = x.Name,
                    Address = x.Address,
                    CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy"),
                    UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy"),
                    Email = x.Email,
                    Phone = x.Phone,
                }).ToListAsync();
            var pagedResult = new PagedResult<GetSupplierWithConvertDate>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetSupplierWithConvertDate>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetSupplierWithConvertDate>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Restore(int id)
        {
            if (id.ToString() == "")
            {
                return new ApiErrorResult<bool>("Khong co Id");
            }
            var findObj = await _dbContext.Suppliers.FindAsync(id);
            if (findObj == null)
            {
                return new ApiErrorResult<bool>("Khong co doi tuong");
            }
            findObj.IsEnable = true;
            _dbContext.Suppliers.Update(findObj);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(int id, SupplierUpdateRequestDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Khong doi tuong");
            }
            var findById = await _dbContext.Suppliers.FindAsync(id);
            if (findById == null)
            {
                return new ApiErrorResult<bool>("Khong co doi tuong");
            }
            findById.Name = request.Name;
            findById.Address = request.Address;
            findById.UpdatedAt = DateTime.Now;
            findById.Email = request.Email;
            findById.Phone = request.Phone;

            _dbContext.Suppliers.Update(findById);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
