using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.ImportInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IImportInvoiceDetailsService
    {
        public Task<ApiResult<bool>> Create(ImportInvoiceDetailsDto request);
        public Task<ApiResult<bool>> Update(int id, ImportInvoiceDetailsDto request);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<ImportInvoiceDetailsDto>>> GetAll(int? pageSize, int? pageIndex);
        public Task<ApiResult<PagedResult<ImportInvoiceDetailsDto>>> GetByName(int? pageSize, int? pageIndex, string? name);
        public Task<ApiResult<ImportInvoiceDetailsDto>> GetById(int Id);
    }
}
