using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Male_Fashion.Services;
using RazorWeb.Controllers;
using Domain.Models.Dto.Blog;

namespace Male_Fashion.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IProductService  _productService;
        private readonly IConfiguration _configuration;
        public HomeController(IBlogService blogService, IProductService productService, IConfiguration configuration)
        {
            _blogService = blogService;
            _productService = productService;
            _configuration = configuration;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> IndexRemake()
        {
            return View();
        }
        public async Task<ActionResult> ProductsHot()
        {
            ViewBag.url = _configuration["BaseAddress"];
            var lst = await _productService.GetSanPhamPagings(8,1,"");
            return PartialView("ProductsHot", lst.ResultObj.Items);
        }
        public async Task<ActionResult> ProductsHot2()
        {
            ViewBag.url = _configuration["BaseAddress"];
            var lst = await _productService.GetSanPhamPagings(8, 2, "");
            return PartialView("ProductsHot2", lst.ResultObj.Items);
        }
        public async Task<ActionResult> News()
        {
            ViewBag.url = _configuration["BaseAddress"];
            try
            {
                var lst = await _blogService.GetPagings(3, 1, "");
                return PartialView("News", lst.ResultObj.Items);
            }
            catch(Exception e)
            {
                ViewBag.Err = e.Message;
                return PartialView("News", new List<BlogVm>());
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public IActionResult GetString()
        {
            string str = _configuration["BaseAddress"];
            return Content(str);
        }
    }
}