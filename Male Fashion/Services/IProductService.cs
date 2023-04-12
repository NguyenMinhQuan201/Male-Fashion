using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Category;
using Domain.Models.Dto.Product;
using Male_Fashion.Models;
using Newtonsoft.Json;

namespace Male_Fashion.Services
{
    public interface IProductService
    {
        Task<ApiResult<PagedResult<GetProductDto>>> GetSanPhamPagings(int? pageSize, int? pageIndex, string? search);
        Task<GetProductDto> GetByIdSanPham(int id);
        Task<ProductDetailDto> GetByIdProductDetail(int id);
        Task<ApiResult<List<ProductDetailDto>>> GetAllDetailByIdPoduct(int id);
        Task<ApiResult<PagedResult<GetProductDto>>> GetProductWithCatePagings(int? pageSize, int? pageIndex, string? search);
        Task<List<Color>> ListAllColor(List<ProductDetailDto> pro);
        Task<List<Size>> ListAllSize(List<ProductDetailDto> pro);
        Task<IEnumerable<CategoryRequestDto>> GetAllCate();
    }
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CategoryRequestDto>> GetAllCate()
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
                var response = await client.GetAsync
                    ($"/api/Category/get-full");
                var body = await response.Content.ReadAsStringAsync();
                var sanpham = JsonConvert.DeserializeObject<IEnumerable<CategoryRequestDto>>(body);
                return sanpham;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<ApiResult<List<ProductDetailDto>>> GetAllDetailByIdPoduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
                var response = await client.GetAsync
                    ($"/api/Product/get-all-detail-with-productid?id={id}");
                var body = await response.Content.ReadAsStringAsync();
                var sanpham = JsonConvert.DeserializeObject<ApiSuccessResult<List<ProductDetailDto>>>(body);
                return sanpham;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<ProductDetailDto> GetByIdProductDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
                var response = await client.GetAsync
                    ($"/api/Product/get-by-id-detail?id={id}");
                var body = await response.Content.ReadAsStringAsync();
                var sanpham = JsonConvert.DeserializeObject<ProductDetailDto>(body);
                return sanpham;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<GetProductDto> GetByIdSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
                var response = await client.GetAsync
                    ($"/api/Product/get-by-id?id={id}");
                var body = await response.Content.ReadAsStringAsync();
                var sanpham = JsonConvert.DeserializeObject<GetProductDto>(body);
                return sanpham;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            throw new NotImplementedException();
        }

        public Task<ApiResult<PagedResult<GetProductDto>>> GetProductWithCatePagings(int? pageSize, int? pageIndex, string? search)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<GetProductDto>>> GetSanPhamPagings(int? pageSize, int? pageIndex, string? search)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var response = await client.GetAsync
                ($"/api/Product/get-all?pageSize={pageSize}&pageIndex={pageIndex}&search={search}");
            var body = await response.Content.ReadAsStringAsync();
            var sanpham = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<GetProductDto>>>(body);
            return sanpham;
        }

        public async Task<PagedResult<GetProductDto>> GetSanPhamPagingsByIdLoaiSanPham(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Color>> ListAllColor(List<ProductDetailDto> pro)
        {
            int i = 0;
            List<Color> result = new List<Color>();
            foreach (var item in pro)
            {
                var color = new Color()
                {
                    Name=item.Color,
                    Id=i++
                };
                result.Add(color);
            }
            return result.DistinctBy(x => x.Name).ToList();
        }

        public async Task<List<Size>> ListAllSize(List<ProductDetailDto> pro)
        {
            int i = 0;
            List<Size> result = new List<Size>();
            foreach (var item in pro)
            {
                var size = new Size()
                {
                    Name = item.Size,
                    Id = i++
                };
                result.Add(size);
            }
            return result.DistinctBy(x=>x.Name).ToList();
        }
    }
}
