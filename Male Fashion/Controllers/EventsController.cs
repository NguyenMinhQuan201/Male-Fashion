using Male_Fashion.Models;
using Male_Fashion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorWeb.Controllers;

namespace Male_Fashion.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IProductService _productService;
        public EventsController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ActionResult> Index(int? pageIndex, string? search,int ? idCate=0, string? branding="", long priceMin=0, long priceMax=0)
        {
            var list = await _productService.GetAllCate();
            ViewBag.data = new SelectList(list, "IdCategory", "Name");
            ViewBag.data2 = new SelectList(list.ToList());
            if (pageIndex == null) pageIndex = 1;
            
            if(idCate == null)
            {
                idCate = 0;
                TempData["idCate"] = 0;
            }
            TempData["branding"] = branding;
            TempData["priceMin"] = priceMin;
            TempData["priceMax"] = priceMax;
            var data = await _productService.GetProductWithCatePagings(4, pageIndex, idCate, search, branding, priceMin, priceMax);
            return View(data.ResultObj);
            //if (idCate > 0 || Convert.ToInt32(val)>0 )
            //{
            //    if (Convert.ToInt32(val) > 0)
            //    {
            //        idCate = Convert.ToInt32(val);
            //    }
            //    var data = await _productService.GetProductWithCatePagings(4, pageIndex,idCate, search,branding,priceMin,priceMax);
            //    ViewBag["idCate"] = idCate;
            //    return View(data.ResultObj);
            //}
            //var result = await _productService.GetSanPhamPagings(4, pageIndex, search);
            //return View(result.ResultObj);
        }
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return Redirect("Index");
            }
            var productDetails = await _productService.GetAllDetailByIdPoduct(id.Value);
            ViewBag.MauSacSP = new SelectList(await _productService.ListAllColor(productDetails.ResultObj), "Id", "Name", 1);
            ViewBag.KichCoSP = new SelectList(await _productService.ListAllSize(productDetails.ResultObj), "Id", "Name", 1);
            var product = await _productService.GetByIdSanPham(id.Value);
            if (product == null)
            {
                return Redirect("Index");
            }
            return View(product);
        }
        public async Task<JsonResult> Checkbysize(string size, int id)
        {
            List<Color> arr = new List<Color>();
            var productDetails = await _productService.GetAllDetailByIdPoduct(id);
            var result = productDetails.ResultObj.Where(x => x.Size == size).Select(x => x.Color).ToList();
            return Json(new
            {
                status = true,
                Arr = result
            });
        }
        public async Task<JsonResult> CheckProduct(int id, string color, string size)
        {
            var productDetails = await _productService.GetAllDetailByIdPoduct(id);
            var result = productDetails.ResultObj.Where(x => x.Size == size && x.Color == color && x.Quantity > 0 && x.ProductId == id).FirstOrDefault();
            if (result == null)
            {

                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {

                status = true,
            });
        }
        public async Task<IActionResult> MoreToYou(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var result = await _productService.GetSanPhamPagings(pageSize, pageNumber, "");
            return PartialView(result.ResultObj.Items);
        }
    }
}
