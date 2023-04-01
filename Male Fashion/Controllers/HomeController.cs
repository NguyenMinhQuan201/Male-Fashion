/*using AdminWeb.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorWeb.Models;
using System.Diagnostics;

namespace RazorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPISanPham _aPISanPham;
        private readonly IAPITinTuc _aPITinTuc;
        public HomeController(IAPISanPham aPISanPham, IAPITinTuc aPITinTuc)
        {
            _aPISanPham = aPISanPham;
            _aPITinTuc = aPITinTuc;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> ProductsHot()
        {
            var lst = await _aPISanPham.GetSanPhamPagings();
            return PartialView("ProductsHot",lst.data);
        }
        public async Task<ActionResult> News()
        {
            *//*var lst = await _db.TinTucs.OrderByDescending(x => x.CreatedDate).Take(3).ToListAsync();*//*
            var lst = await _aPITinTuc.GetPagings();
            return PartialView("News",lst.data);
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
}*/