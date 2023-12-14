using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Blog;
using Newtonsoft.Json;

namespace Male_Fashion.Services
{
    public interface IBlogService
    {
        Task<ApiResult<PagedResult<BlogVm>>> GetPagings(int? pageSize, int? pageIndex, string? search);
    }
    public class BlogService : IBlogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<PagedResult<BlogVm>>> GetPagings(int? pageSize, int? pageIndex, string? search)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            //var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync
                ($"/api/Blogs/get-by-name-blog?pageSize={pageSize}&pageIndex={pageIndex}&name={search}");
            var body = await response.Content.ReadAsStringAsync();
            var sanpham = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<BlogVm>>>(body);
            return sanpham;
        }
    }
}
