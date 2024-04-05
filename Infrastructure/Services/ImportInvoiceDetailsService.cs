using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.Blog;
using Domain.Models.Dto.ImportInvoice;
using Infrastructure.Reponsitories.ImportInvoiceDetailsReponsitories;
using Infrastructure.Reponsitories.ModuleReponsitories;
using Infrastructure.Reponsitories.ProductReponsitories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ImportInvoiceDetailsService : IImportInvoiceDetailsService
    {
        private readonly IProductRepository _productReponsitories;
        private readonly IImportInvoiceDetailsRepository _importInvoiceDetailsRepository;
        private readonly IMapper _mapper;

        public ImportInvoiceDetailsService(IImportInvoiceDetailsRepository importInvoiceDetailsRepository, IProductRepository productReponsitories, IMapper mapper)
        {
            _productReponsitories=productReponsitories;
            _importInvoiceDetailsRepository = importInvoiceDetailsRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<bool>> Create(ImportInvoiceDetailsDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Doi tuong khong ton tai");
            }
            var obj = new Infrastructure.Entities.ImportInvoiceDetails()
            {
                Price = request.Price,
                HSD = request.HSD,
                NSX = request.NSX,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                Discount = request.Discount,
                IdImportInvoice= request.IdImportInvoice,
                IdProduct= request.IdProduct,
                Quantity= request.Quantity,
                Weight= request.Weight,
            };
            await _importInvoiceDetailsRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id >= 0)
            {
                var findobj = await _importInvoiceDetailsRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _importInvoiceDetailsRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<ApiResult<PagedResult<ImportInvoiceDetailsDto>>> GetAll(int? pageSize, int? pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<ImportInvoiceDetailsDto>> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<ImportInvoiceDetailsDto>>> GetByName(int? pageSize, int? pageIndex, string? name)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _importInvoiceDetailsRepository.CountAsync();
            var query = await _importInvoiceDetailsRepository.GetAll(pageSize, pageIndex);
            //if (!string.IsNullOrEmpty(name))
            //{
            //    Expression<Func<Infrastructure.Entities.ImportInvoiceDetails, bool>> expression2 = x => x.Name.Contains(name) && x.Status == true;
            //    query = await _importInvoiceDetailsRepository.GetAll(pageSize, pageIndex, expression2);
            //    totalRow = await _importInvoiceDetailsRepository.CountAsync(expression2);
            //}
            var data = query
                .Select(x => new ImportInvoiceDetailsDto()
                {
                    Price = x.Price,
                    HSD = x.HSD,
                    NSX = x.NSX,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Discount = x.Discount,
                    IdImportInvoice = x.IdImportInvoice,
                    IdProduct = x.IdProduct,
                    Quantity = x.Quantity,
                    Weight = x.Weight,
                    ProductName = _productReponsitories.GetById(x.IdProduct) == null ? _productReponsitories.GetById(x.IdProduct).Result.Name : "Không tên"
                }).ToList();
            var pagedResult = new PagedResult<ImportInvoiceDetailsDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ImportInvoiceDetailsDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ImportInvoiceDetailsDto>>(pagedResult);
        }

        public Task<ApiResult<PagedResult<ImportInvoiceDetailsDto>>> GetDeletedSupplier(int? pageSize, int? pageIndex, string name)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, ImportInvoiceDetailsDto request)
        {
            if (id >= 0)
            {
                var findobj = await _importInvoiceDetailsRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.IdProduct = request.IdProduct;
                findobj.Price = request.Price;
                findobj.Discount = request.Discount;
                findobj.Quantity = request.Quantity;
                findobj.NSX = request.NSX;
                findobj.HSD = request.HSD;
                findobj.Weight = request.Weight;
                findobj.UpdatedAt = DateTime.Now;

                await _importInvoiceDetailsRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
