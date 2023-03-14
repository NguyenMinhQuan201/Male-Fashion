using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Common.FileStorage;
using Domain.Models.Dto.Product;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.CategoryReponsitories;
using Infrastructure.Reponsitories.ProductDetailReponsitories;
using Infrastructure.Reponsitories.ProductImageReponsitories;
using Infrastructure.Reponsitories.ProductReponsitories;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Domain.Features.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productReponsitories;
        private readonly IProductDetailReponsitories _productDetailReponsitories;
        private readonly ICategoryRepository _categoryReponsitories;
        private readonly IProductImageRepository _productImageReponsitories;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper ,ICategoryRepository categoryReponsitories, IProductImageRepository productImageReponsitories, IProductRepository productReponsitories, IStorageService storageService, IProductDetailReponsitories productDetailReponsitories)
        {
            _productDetailReponsitories = productDetailReponsitories;
            _productReponsitories = productReponsitories;
            _storageService = storageService;
            _categoryReponsitories=categoryReponsitories;
            _productDetailReponsitories=productDetailReponsitories;
            _mapper = mapper; ;
        }

        public async Task<ApiResult<bool>> AddImage(int productId, List<IFormFile> request)
        {
            if (productId == null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Product()
                {

                };
                var temp = new List<ProductImg>();
                if (request != null)
                {
                    foreach (var img in request)
                    {
                        var _productImage = new ProductImg()
                        {
                            Caption = findobj.Name,
                            DateCreated = DateTime.Now,
                            FileSize = img.Length,
                            ImagePath = await this.SaveFile(img),
                            IsDefault = true,
                        };
                        temp.Add(_productImage);
                    }
                    findobj.ProductImgs = temp;
                }
                await _productReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task AddViewCount(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(ProductImage request)
        {
            var product = new Infrastructure.Entities.Product()
            {
                Price = request.Price,
                Name = request.Name,
                Quantity = request.Quantity,
                CreatedAt = DateTime.Now,
                Status = request.Status,
                IdCategory = request.IdCategory,
            };
            var temp = new List<ProductImg>();

            if (request.Img != null)
            {
                foreach (var img in request.Img)
                {
                    var _productImage = new ProductImg()
                    {
                        Caption = request.Name,
                        DateCreated = DateTime.Now,
                        FileSize = img.Length,
                        ImagePath = await this.SaveFile(img),
                        IsDefault = true,
                    };
                    temp.Add(_productImage);
                }
                product.ProductImgs = temp;
            }
            await _productReponsitories.CreateAsync(product);
            return product.IdProduct;
        }

        public async Task<ApiResult<ProductDetailDto>> CreateDetailProduct(ProductDetailDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<ProductDetailDto>("Lỗi tham số chuyền về null hoặc trống");
            }
            var productDetail = new Infrastructure.Entities.ProductDetail()
            {
                Price = request.Price,
                Quantity = request.Quantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                NSX = request.NSX,
                HSD = request.HSD,
                Status = request.Status,
                Discount = request.Discount,
                ProductId = request.ProductId,
            };
            await _productDetailReponsitories.CreateAsync(productDetail);
            return new ApiSuccessResult<ProductDetailDto>(request);
        }

        public async Task<ApiResult<bool>> Delete(int productId)
        {
            if (productId == null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Product()
                {
                    Status = false

                };
                await _productReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<List<ProductImage>> GetAll(string languageId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<GetProductDto>>> GetAllbyCategoryId(int? pageSize, int? pageIndex, int?id)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _productReponsitories.CountAsync();
            var query = await _productReponsitories.GetAllProduct(pageSize, pageIndex);
            if (id!=null)
            {
                query = await _productReponsitories.GetAllByCategoryId(pageSize, pageIndex, id );
                totalRow = await _productReponsitories.CountAsyncById(id);
            }
            var data = query
                .Select(x => new GetProductDto()
                {
                    IdProduct = x.IdProduct,
                    Price = x.Price,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = x.Status,
                    IdCategory = x.IdCategory,
                    ImageDtos = _mapper.Map<List<ImageDto>>(x.ProductImgs),
                }).ToList();
            var pagedResult = new PagedResult<GetProductDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetProductDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetProductDto>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<GetProductDto>>> GetAllPaging(int? pageSize, int? pageIndex, string?search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _productReponsitories.CountAsync();
            var query = await _productReponsitories.GetAllProduct(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Product, bool>> expression = x => x.Name.Contains(search);
                query = await _productReponsitories.GetAll(pageSize, pageIndex, expression);
                totalRow = await _productReponsitories.CountAsync(expression);
            }
            var data = query
                .Select(x => new GetProductDto()
                {
                    IdProduct = x.IdProduct,
                    Price = x.Price,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = x.Status,
                    IdCategory = x.IdCategory,
                    ImageDtos = _mapper.Map<List<ImageDto>>(x.ProductImgs)
                }).ToList();
            var pagedResult = new PagedResult<GetProductDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetProductDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetProductDto>>(pagedResult);
        }

        public async Task<GetProductDto> GetById(int productId, string langugeId)
        {
            var findobj = await _productReponsitories.GetByProductID(productId);
            if (findobj == null)
            {
                return null;
            }

            var obj = new GetProductDto()
            {
                IdProduct = findobj.IdProduct,
                Price = findobj.Price,
                Name = findobj.Name,
                Quantity = findobj.Quantity,
                CreatedAt = findobj.CreatedAt,
                UpdatedAt = findobj.UpdatedAt,
                Status = findobj.Status,
                IdCategory = findobj.IdCategory,
                ImageDtos = _mapper.Map<List<ImageDto>>(findobj.ProductImgs),
            };
            return obj;
        }

        public async Task<ImageDto> GetImageByID(int imageId)
        {
            var findobj = await _productImageReponsitories.GetById(imageId);
            if (findobj == null)
            {
                return null;
            }
            return new ImageDto()
            {
                Id=findobj.Id,
                ImagePath = findobj.ImagePath,
                Caption = findobj.Caption,
                DateCreated = findobj.DateCreated,
                FileSize = findobj.FileSize,
                IsDefault = findobj.IsDefault,
                ProductId = findobj.ProductId,
            };
        }

        public async Task<ApiResult<PagedResult<ImageDto>>> GetListImages(int productId)
        {
            
            var totalRow = await _productImageReponsitories.CountAsync();
            var query = await _productImageReponsitories.GetAll(100, 1);
            var data = query
                .Select(x => new ImageDto()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    Caption = x.Caption,
                    DateCreated =x.DateCreated,
                    FileSize = x.FileSize,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId,
                }).ToList();
            var pagedResult = new PagedResult<ImageDto>()
            {
                TotalRecord = totalRow,
                PageSize = 100,
                PageIndex = 1,
                Items = data
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ImageDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ImageDto>>(pagedResult);
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _productImageReponsitories.GetById(imageId);
            if (productImage == null)
            {
                return 0;
            }
            await _productImageReponsitories.DeleteAsync(productImage);
            return 1;
        }

        public Task<int> Update(ProductImage request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int imageId, ImageDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> UpdatePrice(int productId, decimal newPrice)
        {
            if (productId == null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Product()
                {
                    Price = newPrice
                };
                await _productReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> UpdateProductWithoutImage(int productId, ProductImage request)
        {
            if (productId == null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Product()
                {
                    Price = request.Price,
                    Name = request.Name,
                    Quantity = request.Quantity,
                    UpdatedAt = DateTime.Now,
                    Status = request.Status,
                    IdCategory = request.IdCategory,
                };
                await _productReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> UpdateStock(int productId, int addedQuantity)
        {
            if (productId == null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                var obj = new Infrastructure.Entities.Product()
                {
                    Quantity = addedQuantity

                };
                await _productReponsitories.UpdateAsync(obj);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
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
