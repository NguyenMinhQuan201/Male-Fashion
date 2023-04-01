/*using AdminWeb.Services;
using BraintreeHttp;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using PayPal.Core;
using PayPal.v1.Payments;
using RazorWeb.Models;
using Order = RazorWeb.Models.Order;

namespace RazorWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IAPIHoaDons _aPIHoaDons;
        private readonly IAPIChiTietSanPham _aPIChiTietSanPham;
        public OrderController(IConfiguration configuration, IAPIHoaDons aPIHoaDons, IAPIChiTietSanPham aPIChiTietSanPham)
        {
            _configuration = configuration;
            _aPIHoaDons = aPIHoaDons;
            _aPIChiTietSanPham = aPIChiTietSanPham;
        }
        public IActionResult Index()
        {
            return View();
        }
        private async void makeDetail(string cartUser, int orderID)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<Order>>(cartUser);
            var findOrder = *//*_db.HoaDons.Where(x => x.IDHoaDon == orderID).FirstOrDefault();*//* await _aPIHoaDons.GetById(orderID);

            int i = 0;
            foreach (var item in jsoncart)
            {
                i = i + 1;
                var orderDetail = new ChiTietHoaDon()
                {
                    IDHoaDon = findOrder.data.IDHoaDon,
                    Gia = item.Gia,
                    Images = item.Img,
                    MauSacSP = item.Mau,
                    KichCoSP = item.Kich,
                    Soluong = item.SoLuong,
                };
                *//*_db.ChiTietHoaDons.Add(orderDetail);*//*
                try
                {
                    await _aPIHoaDons.CreateCTHD(orderDetail);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public async Task<JsonResult> MakeOrder(string cartUser, string addRess, int phone)
        {
            try
            {
                var jsoncart = new JavaScriptSerializer().Deserialize<List<Order>>(cartUser);
                List<decimal> GiaNhap = new List<decimal>();
                decimal tong = 0;
                decimal tonggianhap = 0;
                foreach (var item in jsoncart)
                {
                    tong = tong + (decimal)item.Tong;
                    var findGiaNhapById = await _aPIChiTietSanPham.GetByIdChiTietSanPham(item.Prime);
                    tonggianhap = tonggianhap + (decimal)findGiaNhapById.data.GiaNhap * item.SoLuong;
                }

                var order = new HoaDon()
                {
                    TongGiaNhap = tonggianhap,
                    Gia = tong,
                    SDT = phone,
                    DiaChi = addRess,
                    NgayGio = DateTime.UtcNow
                };
                await _aPIHoaDons.Create(order);
                return Json(new { status = true });
            }
            catch (Exception)
            {
                Console.WriteLine("Lỗi");
                return Json(new { status = false });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PaymentWithPaypal(string cartUser, string addRess, int phone)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<Order>>(cartUser);
            var _clientId = _configuration["Paypal:ClientId"];
            var _secretKey = _configuration["Paypal:SecretKey"];
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);
            var Tygia = 23300;
            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = jsoncart.Sum(p => p.Tong);
            *//*var total = Math.Round(100000 / Tygia, 2);*//*
            var tax = 1;
            var shipping = 1;
            foreach (var item in jsoncart)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.Name,
                    Currency = "USD",
                    *//*Price = Math.Round(item.DonGia / TyGiaUSD, 2).ToString(),*//*
                    Price = Math.Round(item.Gia).ToString(),
                    Quantity = item.SoLuong.ToString(),
                    Sku = "sku",
                });
            }
            #endregion

            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {

                    new Transaction()
                    {

                        Amount = new Amount()
                        {
                            Total = (Convert.ToDouble(tax)+Convert.ToDouble(shipping)+Convert.ToDouble(total)).ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = tax.ToString(),
                                Shipping = shipping.ToString(),
                                Subtotal = total.ToString()
                            },
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/Paypal/CheckoutFail",
                    ReturnUrl = $"{hostname}/Paypal/CheckoutSuccess"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Json(new { link = paypalRedirectUrl, status = true });
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Paypal/CheckoutFail");
            }
        }
        public async Task<JsonResult> MakeOrderPaypal(string cartUser, string thongtin)
        {
            try
            {
                var jsoncart = new JavaScriptSerializer().Deserialize<List<Order>>(cartUser);
                var jsonOrder = new JavaScriptSerializer().Deserialize<List<ThongTin>>(thongtin);
                List<decimal> GiaNhap = new List<decimal>();
                decimal tong = 0;
                decimal tonggianhap = 0;
                foreach (var item in jsoncart)
                {
                    tong = tong + (decimal)item.Tong;
                    var findGiaNhapById = *//*_db.ChiTietSanPhams.Find(item.Prime);*//* await _aPIChiTietSanPham.GetByIdChiTietSanPham(item.Prime);
                    tonggianhap = tonggianhap + (decimal)findGiaNhapById.data.GiaNhap * item.SoLuong;
                }

                var order = new HoaDon()
                {
                    TongGiaNhap = tonggianhap,
                    Gia = tong,
                    SDT = jsonOrder[0].phone,
                    DiaChi = jsonOrder[0].address,
                    NgayGio = DateTime.UtcNow
                };
                *//*_db.HoaDons.Add(order);
                _db.SaveChanges();*//*
                var rs = await _aPIHoaDons.Create(order);
                makeDetail(cartUser, rs.data.IDHoaDon);
                return Json(new { status = true });
            }
            catch (Exception)
            {
                Console.WriteLine("loi");
                return Json(new { status = false });
            }
        }
    }
}
*/