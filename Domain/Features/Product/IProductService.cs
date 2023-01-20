using Domain.Common;
using Domain.Models.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Product
{
    public interface IProductService
    {
        Task<int> Create(ProductDto request);
        Task<int> Update(ProductDto request);
        Task<int> Delete(int productId);
        Task<ProductDto> GetById(int productId, string langugeId);
        Task<bool> UpdatePrice(int ProductId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<PagedResult<ProductDto>> GetAllPaging(int? pageSize, int? pageIndex, string search);
        Task<int> AddImage(int productId, ImageDto request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ImageDto request);
        Task<List<ImageDto>> GetListImages(int productId);
        Task<ImageDto> GetImageByID(int imageId);
        Task<PagedResult<ProductDto>> GetAllbyCategoryId(string languageId, int? pageSize, int? pageIndex, string search);
        Task<List<ProductDto>> GetAll(string languageId);
    }
}
