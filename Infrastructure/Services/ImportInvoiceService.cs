using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.Blog;
using Domain.Models.Dto.ImportInvoiceDto;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.ImportInvoiceDetailsReponsitories;
using Infrastructure.Reponsitories.ImportInvoiceReponsitories;
using Infrastructure.Reponsitories.ProductReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ImportInvoiceService : IImportInvoiceService
    {
        private readonly IImportInvoiceRepository _importInvoiceRepository;
        private readonly IMapper _mapper;

        public ImportInvoiceService(IImportInvoiceRepository importInvoiceRepository, IMapper mapper)
        {
            _importInvoiceRepository = importInvoiceRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<bool>> Create(ImportInvoiceDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Doi tuong khong ton tai");
            }
            var obj = new Infrastructure.Entities.ImportInvoice()
            {
                Name = request.Name,
                SumPrice = request.SumPrice,
                Address = request.Address,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                Phone = request.Phone,
                Note = request.Note,
                Status = request.Status,
                CreatAtBy = "",
                IdSupplier = request.IdSupplier,
                IdImportInvoice = request.IdImportInvoice,
            };
            await _importInvoiceRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id >= 0)
            {
                var findobj = await _importInvoiceRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _importInvoiceRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<ApiResult<PagedResult<ImportInvoiceDto>>> GetAll(int? pageSize, int? pageIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<ImportInvoiceDto>> GetById(int id)
        {
            if (id != null)
            {
                var findobj = await _importInvoiceRepository.GetById(id);
                if (findobj == null)
                {
                    return null;
                }

                var obj = new ImportInvoiceDto()
                {
                    Name = findobj.Name,
                    SumPrice = findobj.SumPrice,
                    Address = findobj.Address,
                    CreatedAt = findobj.CreatedAt,
                    UpdatedAt = findobj.UpdatedAt,
                    Phone = findobj.Phone,
                    Note = findobj.Note,
                    Status = findobj.Status,
                    CreatAtBy = "",
                };
                return new ApiSuccessResult<ImportInvoiceDto>(obj);
            }
            return new ApiErrorResult<ImportInvoiceDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<ImportInvoiceDto>>> GetByName(int? pageSize, int? pageIndex, string? name)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _importInvoiceRepository.CountAsync();
            var query = await _importInvoiceRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(name))
            {
                Expression<Func<Infrastructure.Entities.ImportInvoice, bool>> expression2 = x => x.Name.Contains(name);
                query = await _importInvoiceRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _importInvoiceRepository.CountAsync(expression2);
            }
            var data = query
                .Select(x => new ImportInvoiceDto()
                {
                    Name = x.Name,
                    SumPrice = x.SumPrice,
                    Address = x.Address,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Phone = x.Phone,
                    Note = x.Note,
                    Status = x.Status,
                    CreatAtBy = "",
                }).ToList();
            var pagedResult = new PagedResult<ImportInvoiceDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ImportInvoiceDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ImportInvoiceDto>>(pagedResult);
        }

        public Task<ApiResult<PagedResult<ImportInvoiceDto>>> GetDeletedSupplier(int? pageSize, int? pageIndex, string name)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, ImportInvoiceDto request)
        {
            if (id >= 0)
            {
                var findobj = await _importInvoiceRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Name = request.Name;
                findobj.SumPrice = request.SumPrice;
                findobj.Address = request.Address;
                findobj.Phone = request.Phone;
                findobj.Note = request.Note;
                findobj.Status = "";
                findobj.CreatAtBy = request.CreatAtBy;
                findobj.UpdatedAt = DateTime.Now;

                await _importInvoiceRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
