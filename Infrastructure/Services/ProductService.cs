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
using Infrastructure.Reponsitories.RatingReponsitories;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Domain.Features.Product
{
    public class ProductService : IProductService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IProductRepository _productReponsitories;
        private readonly IProductDetailReponsitories _productDetailReponsitories;
        private readonly ICategoryRepository _categoryReponsitories;
        private readonly IStorageService _storageService;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;
        public ProductService(IRatingRepository ratingRepository,IProductImageRepository productImageRepository, IMapper mapper, ICategoryRepository categoryReponsitories, IProductImageRepository productImageReponsitories, IProductRepository productReponsitories, IStorageService storageService, IProductDetailReponsitories productDetailReponsitories)
        {
            _productDetailReponsitories = productDetailReponsitories;
            _productReponsitories = productReponsitories;
            _storageService = storageService;
            _categoryReponsitories = categoryReponsitories;
            _productImageRepository = productImageReponsitories;
            _ratingRepository = ratingRepository;
            _mapper = mapper; ;
        }
        public async Task<ApiResult<bool>> AddImage(int productId, List<IFormFile> request)
        {
            if (productId != null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
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
                await _productReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task AddViewCount(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Create(ProductDto request)
        {
            Expression<Func<Infrastructure.Entities.Product, bool>> expression = x => x.Name == request.Name;
            var check = await _productReponsitories.FindByName(expression);
            if (check == null)
            {
                var product = new Infrastructure.Entities.Product()
                {
                    Price = request.Price,
                    Name = request.Name,
                    Quantity = request.Quantity,
                    CreatedAt = DateTime.Now,
                    Status = request.Status,
                    IdCategory = request.IdCategory,
                    Description = request.Description,
                    Branding=""
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
                return new ApiSuccessResult<bool>(true);
            }
            else
            {
                return new ApiErrorResult<bool>("Product name has existed");
            }
        }

        public async Task<ApiResult<ProductDetailDto>> CreateDetailProduct(ProductDetailDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<ProductDetailDto>("Lỗi tham số chuyền về null hoặc trống");
            }
            var productDetail = new Infrastructure.Entities.ProductDetail()
            {
                Quantity = request.Quantity,
                Status = request.Status,
                ProductId = request.ProductId,
                Size = request.Size,
                Color = request.Color,
                CreatedAt = DateTime.Now,
                ProductName = request.ProductName,
            };
            await _productDetailReponsitories.CreateAsync(productDetail);
            return new ApiSuccessResult<ProductDetailDto>(request);
        }

        public async Task<ApiResult<bool>> Delete(int productId)
        {
            if (productId != null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = false;
                await _productReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> DeleteDetail(int id)
        {
            if (id != null)
            {
                var findobj = await _productDetailReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = 0;
                await _productDetailReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<List<ProductDto>> GetAll(string languageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetProductDto>> GetAll()
        {
            var result = await _productReponsitories.GetAll();
            return result.Select(x => new GetProductDto()
            {
                IdProduct = x.IdProduct,
                Name = x.Name,

            }).ToList();
        }

        public async Task<ApiResult<PagedResult<GetProductDto>>> GetAllbyCategoryId(int? pageSize, int? pageIndex, int? id, string? search, string? branding, long priceMin, long priceMax)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _productReponsitories.CountByCateIdAsync(pageSize, pageIndex, id, search, branding, priceMin, priceMax);
            var query = await _productReponsitories.GetAllByCategoryId(pageSize, pageIndex, id, search, branding, priceMin,priceMax);
            var data = _mapper.Map<List<GetProductDto>>(query.ToList());
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

        public async Task<ApiResult<List<ProductDetailDto>>> GetAllDetailByIdPoduct(int id)
        {
            if (id != null)
            {
                var findobj = await _productReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<List<ProductDetailDto>>("Không tìm thấy đối tượng");
                }
                Expression<Func<Infrastructure.Entities.ProductDetail, bool>> expression = x => x.ProductId == id;
                var totalRow = await _productDetailReponsitories.CountAsync(expression);
                var query = await _productDetailReponsitories.GetByCondition(expression);
                var data = query
                .Select(x => new ProductDetailDto()
                {
                    Id = x.Id,
                    Color = x.Color,
                    Size = x.Size,
                    Quantity = x.Quantity,
                    Status = x.Status,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                }).ToList();
                return new ApiSuccessResult<List<ProductDetailDto>>(data);
            }
            return new ApiErrorResult<List<ProductDetailDto>>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<GetProductDto>>> GetAllPaging(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Product, bool>> expression1 = x => x.Status == true;
            var totalRow = await _productReponsitories.CountAsync(expression1);
            var query = await _productReponsitories.GetAllProduct(pageSize, pageIndex, expression1);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Product, bool>> expression2 = x => x.Name.Contains(search) && x.Status == true;
                query = await _productReponsitories.GetAllProduct(pageSize, pageIndex, expression2);
                totalRow = await _productReponsitories.CountAsync(expression2);
            }

            var data = _mapper.Map<List<GetProductDto>>(query.ToList());
            var pagedResult = new PagedResult<GetProductDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<GetProductDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<GetProductDto>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<ProductDetailDto>>> GetAllPagingDetail(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.ProductDetail, bool>> expression = x => x.Status == 1;
            var totalRow = await _productDetailReponsitories.CountAsync(expression);
            var query = await _productDetailReponsitories.GetAll(pageSize, pageIndex, expression);
            var data = query
                .Select(x => new ProductDetailDto()
                {
                    Id = x.Id,
                    Color = x.Color,
                    Size = x.Size,
                    Quantity = x.Quantity,
                    Status = x.Status,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                }).ToList();
            var pagedResult = new PagedResult<ProductDetailDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ProductDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ProductDetailDto>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<ProductDetailDto>>> GetAllPagingDetailRemoved(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.ProductDetail, bool>> expression1 = x => x.Status == 0;
            var totalRow = await _productDetailReponsitories.CountAsync(expression1);
            var query = await _productDetailReponsitories.GetAll(pageSize, pageIndex, expression1);
            var data = query
                .Select(x => new ProductDetailDto()
                {
                    Id = x.Id,
                    Color = x.Color,
                    Size = x.Size,
                    Quantity = x.Quantity,
                    Status = x.Status,
                    ProductName = x.ProductName,
                }).ToList();
            var pagedResult = new PagedResult<ProductDetailDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = false
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ProductDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ProductDetailDto>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<GetProductDto>>> GetAllPagingRemoved(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Product, bool>> expression1 = x => x.Status == false;
            var totalRow = await _productReponsitories.CountAsync(expression1);
            var query = await _productReponsitories.GetAllProduct(pageSize, pageIndex, expression1);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Product, bool>> expression2 = x => x.Name.Contains(search) && x.Status == false;
                query = await _productReponsitories.GetAllProduct(pageSize, pageIndex, expression2);
                totalRow = await _productReponsitories.CountAsync(expression2);
            }
            var data = _mapper.Map<List<GetProductDto>>(query.ToList());
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

        public async Task<GetProductDto> GetById(int productId)
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
                Description = findobj.Description,
                ProductImgs = _mapper.Map<List<ImageDto>>(findobj.ProductImgs),
                Rating = _ratingRepository.GetByCondition(x=>x.Id==findobj.IdProduct).Result.ToList().Select(y=>new Models.Dto.Product.Rating()
                {
                    DateCreate = y.DateCreate,
                    Des=y.Des,
                    Id=y.Id,
                    SDT=y.SDT,
                    Stars=y.Stars,
                    IdOrder=y.IdOrder,
                    Name=y.Name,
                }).ToList(),
            };
            return obj;
        }

        public async Task<ProductDetailDto> GetByIdDetail(int id)
        {
            var findobj = await _productDetailReponsitories.GetById(id);
            if (findobj == null)
            {
                return null;
            }

            var obj = new ProductDetailDto()
            {
                Id = findobj.Id,
                Size = findobj.Size,
                Color = findobj.Color,
                Status = findobj.Status,
                ProductId = findobj.ProductId,
                Quantity = findobj.Quantity,
                ProductName = findobj.ProductName,
            };
            return obj;
        }

        public async Task<ImageDto> GetImageByID(int imageId)
        {
            var findobj = await _productImageRepository.GetById(imageId);
            if (findobj == null)
            {
                return null;
            }
            return new ImageDto()
            {
                Id = findobj.Id,
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

            var totalRow = await _productImageRepository.CountAsync();
            var query = await _productImageRepository.GetAll(100, 1);
            var data = query
                .Select(x => new ImageDto()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
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
            var productImage = await _productImageRepository.GetById(imageId);
            await _storageService.DeleteFileAsync(productImage.ImagePath);
            if (productImage == null)
            {
                return 0;
            }
            await _productImageRepository.DeleteAsync(productImage);
            return 1;
        }

        public async Task<ApiResult<bool>> RestoreProduct(int id)
        {
            if (id != null)
            {
                var findobj = await _productReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = true;
                await _productReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> RestoreProductDetail(int id)
        {
            if (id != null)
            {
                var findobj = await _productDetailReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = 1;
                await _productDetailReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<int> Update(ProductDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> UpdateDetail(int id, ProductDetailDto request)
        {
            if (id != null)
            {
                var findobj = await _productDetailReponsitories.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Color = request.Color;
                findobj.Size = request.Size;
                findobj.Quantity = request.Quantity;
                findobj.UpdatedAt = DateTime.Now;
                findobj.Status = request.Status;
                findobj.ProductId = request.ProductId;
                findobj.ProductName = request.ProductName;
                await _productDetailReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
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

        public async Task<ApiResult<bool>> UpdateProductWithoutImage(int productId, ProductDto request)
        {
            if (productId != null)
            {
                var findobj = await _productReponsitories.GetById(productId);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Price = request.Price;
                findobj.Name = request.Name;
                findobj.Quantity = request.Quantity;
                findobj.UpdatedAt = DateTime.Now;
                findobj.Status = request.Status;
                findobj.IdCategory = request.IdCategory;
                findobj.Description = request.Description;
                await _productReponsitories.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
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
