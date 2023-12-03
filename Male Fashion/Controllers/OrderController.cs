using AutoMapper;
using BraintreeHttp;
using Domain.Models.Dto.Order;
using Male_Fashion.Others;
using Male_Fashion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Nancy.Json;
using Newtonsoft.Json;
using PayPal.Core;
using PayPal.v1.Payments;
using RazorWeb.Others;

namespace RazorWeb.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IConfiguration configuration, IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _configuration = configuration;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> MakeOrder(string cartUser, string addRess, int phone)
        {
            try
            {
                var jsoncart = new JavaScriptSerializer().Deserialize<List<OrderDetailRequest>>(cartUser).Select(x => new OrderDetailDto
                {
                    Discounnt = 0,
                    IdOrder = x.IdOrder,
                    IdProduct = x.Id,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList();
                var Order = new OrderDto()
                {
                    Payments = "Affter receive product",
                    Status = 1,
                    SumPrice = jsoncart.Sum(x => x.Quantity * x.Price),
                    Address = addRess,
                    DeliveryAt = DateTime.Now,
                    NameCustomer = null,
                    Email = null,
                    Note = "",
                    Phone = phone,
                    OrderDetails = jsoncart
                };
                var result = await _orderService.MakeOrder(Order);
                return Json(new { status = true });
            }
            catch (Exception)
            {
                Console.WriteLine("Lỗi");
                return Json(new { status = false });
            }
        }
        public async Task<JsonResult> MakeOrderPayPal(string cartUser, string addRess, int phone)
        {
            try
            {
                var jsoncart = new JavaScriptSerializer().Deserialize<List<OrderDetailRequest>>(cartUser).Select(x => new OrderDetailDto
                {
                    Discounnt = 0,
                    IdOrder = x.IdOrder,
                    IdProduct = x.Id,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList();
                var Order = new OrderDto()
                {
                    Payments = "Paid",
                    Status = 1,
                    SumPrice = jsoncart.Sum(x => x.Quantity * x.Price),
                    Address = addRess,
                    DeliveryAt = DateTime.Now,
                    NameCustomer = null,
                    Email = null,
                    Note = "",
                    Phone = phone,
                    OrderDetails = jsoncart
                };

                var result = await _orderService.MakeOrder(Order);
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
            var jsoncart = new JavaScriptSerializer().Deserialize<List<OrderDetailRequest>>(cartUser).Select(x => new OrderDetailDto
            {
                Discounnt = 0,
                IdOrder = x.IdOrder,
                IdProduct = x.Id,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();
            var Order = new OrderDto()
            {
                Payments = "Paid",
                Status = 1,
                SumPrice = jsoncart.Sum(x => x.Quantity * x.Price),
                Address = addRess,
                DeliveryAt = DateTime.Now,
                NameCustomer = null,
                Email = null,
                Note = "",
                Phone = phone,
                OrderDetails = jsoncart
            };
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
            var total = jsoncart.Sum(x => x.Quantity * x.Price);
            var tax = 1;
            var shipping = 1;
            foreach (var item in jsoncart)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.IdProduct.ToString(),
                    Currency = "USD",
                    Price = Math.Round(item.Price).ToString(),
                    Quantity = item.Quantity.ToString(),
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
                        paypalRedirectUrl = lnk.Href;
                    }
                }
                /*await _orderService.MakeOrder(Order);*/
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
        public ActionResult Payment(string cartUser, string addRess, int phone)
        {
            try
            {
                var jsoncart = new JavaScriptSerializer().Deserialize<List<OrderDetailRequest>>(cartUser).Select(x => new OrderDetailDto
                {
                    Discounnt = 0,
                    IdOrder = x.IdOrder,
                    IdProduct = x.Id,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList();
                var Order = new OrderDto()
                {
                    Payments = "Paid",
                    Status = 1,
                    SumPrice = jsoncart.Sum(x => x.Quantity * x.Price),
                    Address = addRess,
                    DeliveryAt = DateTime.Now,
                    NameCustomer = null,
                    Email = null,
                    Note = "",
                    Phone = phone,
                    OrderDetails = jsoncart
                };
                HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(Order));
                string url = _configuration["VNPAY:Url"];
                string returnUrl = _configuration["VNPAY:ReturnUrl"];
                string tmnCode = _configuration["VNPAY:TmnCode"];
                string hashSecret = _configuration["VNPAY:HashSecret"];
                var IpAdress = ip();
                PayLib pay = new PayLib();
                pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
                pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
                pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
                pay.AddRequestData("vnp_Amount", Convert.ToString(Order.SumPrice*100)); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
                pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
                pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
                pay.AddRequestData("vnp_IpAddr", IpAdress /*Util.GetIpAddress()*/); //Địa chỉ IP của khách hàng thực hiện giao dịch
                pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
                pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
                pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
                pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
                pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn
                string paymentUrl = pay.CreateRequestUrlWithHmacSHA512(url, hashSecret);
                return Json(new { link = paymentUrl, status = true });

            }
            catch (Exception)
            {
                Console.WriteLine("Lỗi");
                return Json(new { status = false });
            }
        }
        public ActionResult PaymentConfirm()
        {
            var deserializedObject = JsonConvert.DeserializeObject<OrderDto>(HttpContext.Session.GetString("Order"));
            _orderService.MakeOrder(deserializedObject);
            if (Request.Query.Count > 0)
            {
                string hashSecret = _configuration["VNPAY:HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.Query.AsEnumerable();
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (var s in vnpayData)
                {
                    pay.AddResponseData(s.Key, s.Value);
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.Query["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignatureHmacSHA512(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return RedirectToAction("CheckoutSuccess", "Paypal");
        }
        
    }
}
