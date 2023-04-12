using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Male_Fashion.Services;

namespace Male_Fashion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IProductService  _productService;
        public HomeController(IBlogService blogService, IProductService productService)
        {
            _blogService = blogService;
            _productService = productService;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> ProductsHot()
        {
            var lst = await _productService.GetSanPhamPagings(8,1,"");
            return PartialView("ProductsHot", lst.ResultObj.Items);
        }
        public async Task<ActionResult> News()
        {
            var lst = await _blogService.GetPagings(3, 1, "");
            return PartialView("News", lst.ResultObj.Items);
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
    }
}