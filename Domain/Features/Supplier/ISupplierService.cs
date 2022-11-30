using DataDemo.Common;
using Domain.Common;
using Domain.Features.Supplier.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Features.ManageSuppliers
{
    public interface ISupplierService
    {
        public Task<ApiResult<bool>> Create(SupplierCreateRequestDto request);
        public Task<ApiResult<bool>> Update(int id, SupplierUpdateRequestDto request);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<GetSupplier>>> GetAll(int? pageSize, int? pageIndex);
        public Task<ApiResult<PagedResult<GetSupplierWithConvertDate>>> GetByName(int? pageSize, int? pageIndex, string?name);
        public Task<ApiResult<GetSupplier>> GetById(int Id);
        public Task<ApiResult<PagedResult<GetSupplierWithConvertDate>>> GetDeletedSupplier(int? pageSize, int? pageIndex, string name);
        /*public Task<UserVmDto> GetById(Guid id);*/
    }
}
