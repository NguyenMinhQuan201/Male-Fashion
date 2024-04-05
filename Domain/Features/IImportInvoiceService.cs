using DataDemo.Common;
using Domain.Common;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.ImportInvoice;
using Domain.Models.Dto.ImportInvoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IImportInvoiceService
    {
        public Task<ApiResult<bool>> Create(ImportInvoiceDto request);
        public Task<ApiResult<bool>> Update(int id, ImportInvoiceDto request);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<ImportInvoiceDto>>> GetAll(int? pageSize, int? pageIndex);
        public Task<ApiResult<PagedResult<ImportInvoiceDto>>> GetByName(int? pageSize, int? pageIndex, string? name);
        public Task<ApiResult<ImportInvoiceDto>> GetById(int Id);
    }
}
