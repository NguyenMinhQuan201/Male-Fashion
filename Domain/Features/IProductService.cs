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
        Task<int> Create(ProductImage request);
        Task<ApiResult<ProductDetailDto>> CreateDetailProduct(ProductDetailDto request);
        Task<ApiResult<bool>> UpdateProductWithoutImage(int productId, ProductImage request);
        Task<int> Update(ProductImage request);
        Task<ApiResult<bool>> Delete(int productId);
        Task<GetProductDto> GetById(int productId, string langugeId);
        Task<ApiResult<bool>> UpdatePrice(int ProductId, decimal newPrice);
        Task<ApiResult<bool>> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<ApiResult<PagedResult<GetProductDto>>> GetAllPaging(int? pageSize, int? pageIndex, string? search);
        Task<ApiResult<bool>> AddImage(int productId, List<IFormFile> request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ImageDto request);
        Task<ApiResult<PagedResult<ImageDto>>> GetListImages(int productId);
        Task<ImageDto> GetImageByID(int imageId);
        Task<ApiResult<PagedResult<GetProductDto>>> GetAllbyCategoryId(int? pageSize, int? pageIndex, int? search);
        Task<List<ProductImage>> GetAll(string languageId);
    }
}
