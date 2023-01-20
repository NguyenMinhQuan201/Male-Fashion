using Domain.Common;
using Domain.Common.FileStorage;
using Domain.Models.Dto.Product;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.ProductReponsitories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductReponsitories _productReponsitories;
        private readonly IStorageService _storageService;
        public ProductService(IProductReponsitories productReponsitories, IStorageService storageService)
        {
            _productReponsitories = productReponsitories;
            _storageService = storageService;
        }

        public Task<int> AddImage(int productId, ImageDto request)
        {
            throw new NotImplementedException();
        }

        public Task AddViewCount(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(ProductDto request)
        {
            var product = new Infrastructure.Entities.Product()
            {
                Price = request.Price,
                Name = request.Name,
                Quantity = request.Quantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = request.Status,
                IdCategory = request.IdCategory,
            };
            if (request.Img != null)
            {
                product.ProductImgs = new List<ProductImg>()
                {
                    new ProductImg()
                    {
                        Caption= request.Name,
                        DateCreated=DateTime.Now,
                        FileSize=request.Img.Length,
                        ImagePath=await this.SaveFile(request.Img),
                        IsDefault=true,
                    }
                };
            }
            await _productReponsitories.CreateAsync(product);
            return product.IdProduct;
        }

        public Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetAll(string languageId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ProductDto>> GetAllbyCategoryId(string languageId, int? pageSize, int? pageIndex, string search)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ProductDto>> GetAllPaging(int? pageSize, int? pageIndex, string search)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetById(int productId, string langugeId)
        {
            throw new NotImplementedException();
        }

        public Task<ImageDto> GetImageByID(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImageDto>> GetListImages(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImage(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductDto request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int imageId, ImageDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePrice(int ProductId, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            throw new NotImplementedException();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
