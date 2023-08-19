using Domain.Models.Dto.LoginDto;
using Male_Fashion.Models;
using Newtonsoft.Json;
using System.Text;

namespace Male_Fashion.Services
{
    public interface IUserService
    {
        Task<LoginResponDto> GetToken(LoginModel rs);
    }
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginResponDto> GetToken(LoginModel rs)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(rs);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync
                ($"/api/User/login", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<LoginResponDto>(body);
            return loaisanpham;
        }
    }
}
