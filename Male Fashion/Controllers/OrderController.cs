using AutoMapper;
using BraintreeHttp;
using Domain.Models.Dto.Order;
using Male_Fashion.Services;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using PayPal.Core;
using PayPal.v1.Payments;

namespace RazorWeb.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrderController(IConfiguration configuration,IMapper mapper,IOrderService orderService)
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
                var jsoncart = new JavaScriptSerializer().Deserialize<List<OrderDetailRequest>>(cartUser).Select(x=> new OrderDetailDto
                {
                    Discounnt=0,
                    IdOrder=x.IdOrder,
                    IdProduct=x.Id,
                    Price=x.Price,
                    Quantity=x.Quantity
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
        
    }
}
