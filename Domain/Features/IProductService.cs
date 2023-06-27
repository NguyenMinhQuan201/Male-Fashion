using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IProductService
    {
        Task<ApiResult<bool>> Create(ProductDto request);
        Task<ApiResult<ProductDetailDto>> CreateDetailProduct(ProductDetailDto request);
        Task<ApiResult<bool>> UpdateProductWithoutImage(int productId, ProductDto request);
        Task<ApiResult<bool>> UpdateDetail(int id, ProductDetailDto request);
        Task<int> Update(ProductDto request);
        Task<ApiResult<bool>> Delete(int productId);
        Task<ApiResult<bool>> DeleteDetail(int id);
        Task<GetProductDto> GetById(int productId);
        Task<ProductDetailDto> GetByIdDetail(int id);
        Task<ApiResult<bool>> UpdatePrice(int ProductId, decimal newPrice);
        Task<ApiResult<bool>> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<ApiResult<List<ProductDetailDto>>> GetAllDetailByIdPoduct(int id);
        Task<IEnumerable<GetProductDto>> GetAll();
        Task<ApiResult<PagedResult<GetProductDto>>> GetAllPaging(int? pageSize, int? pageIndex, string? search);
        Task<ApiResult<PagedResult<ProductDetailDto>>> GetAllPagingDetail(int? pageSize, int? pageIndex, string? search);
        Task<ApiResult<PagedResult<ProductDetailDto>>> GetAllPagingDetailRemoved(int? pageSize, int? pageIndex, string? search);
        Task<ApiResult<PagedResult<GetProductDto>>> GetAllPagingRemoved(int? pageSize, int? pageIndex, string? search);
        Task<int> UpdateImage(int imageId, ImageDto request);
        Task<ApiResult<PagedResult<ImageDto>>> GetListImages(int productId);
        Task<ImageDto> GetImageByID(int imageId);
        Task<ApiResult<PagedResult<GetProductDto>>> GetAllbyCategoryId(int? pageSize, int? pageIndex, int? id,string ? search);
        Task<List<ProductDto>> GetAll(string languageId);
        public Task<ApiResult<bool>> AddImage(int productId, List<IFormFile> request);
        public Task<int> RemoveImage(int imageId);
        public Task<ApiResult<bool>> RestoreProduct(int id);
        public Task<ApiResult<bool>> RestoreProductDetail(int id);
    }
}
