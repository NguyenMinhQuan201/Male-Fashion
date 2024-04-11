using Male_Fashion.Models;
using Male_Fashion.Services;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
namespace RazorWeb.Controllers
{
    public class CartsController : BaseController
    {
        private readonly IProductService _productService;
        public CartsController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<JsonResult> AddCart(int id, string size, string color)
        {
            var productDetails = await _productService.GetAllDetailByIdPoduct(id);
            var product = await _productService.GetByIdSanPham(id);
            var result = productDetails.ResultObj.Where(x => x.Size == size && x.Color == color && x.Quantity > 0 && x.ProductId == id).FirstOrDefault();
            return Json(
            new
            {
                status = true,
                Id = id,
                ImagePath = product.ProductImgs[0].ImagePath,
                Price = product.Price,
                Name = product.Name,
                Color = color,
                Size = size,
                Quantity = result.Quantity,
            });
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> RemakeRender(string cartTemp)
        {
            List<Cart> a = new List<Cart>();
            var jsoncart = new JavaScriptSerializer().Deserialize<List<Cart>>(cartTemp);
            foreach (var item in jsoncart)
            {
                var find = await _productService.GetByIdProductDetail(item.Id);
                if (find.Quantity == 0)
                {
                    item.Status = false;

                }
                else
                {
                    item.Status = true;
                }
                a.Add(item);
            }
            return Json(new { status = true, list = a });
        }
        public async Task<JsonResult> ChangePrice(int id, int quantity)
        {
            var find = await _productService.GetByIdProductDetail(id);
            if (find != null)
            {
                if (find.Quantity < quantity)
                {
                    return Json(
                      new
                      {
                          status = false,
                          max = find.Quantity,
                      });
                }

            }
            return Json(new { status = true });
        }
    }
}
