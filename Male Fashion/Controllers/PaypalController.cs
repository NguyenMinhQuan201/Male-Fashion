using BraintreeHttp;
using Microsoft.AspNetCore.Mvc;
using PayPal.Core;
using PayPal.v1.Payments;

namespace RazorWeb.Controllers
{
    public class PaypalController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaypalController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PaypalCheckout()
        {
            var _clientId = _configuration["Paypal:ClientId"];
            var _secretKey = _configuration["Paypal:SecretKey"];
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);
            double Tygia = 23300;
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = Math.Round(100000 / Tygia, 2);
            var tax = 1;
            var shipping = 1;
            itemList.Items.Add(new Item()
            {
                Name = "test",
                Currency = "USD",
                Price = Math.Round(100000 / Tygia, 2).ToString(),
                Quantity = (1).ToString(),
                Sku = "sku",
                Tax = "0",

            });
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

                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Paypal/CheckoutFail");
            }
        }

        public IActionResult CheckoutFail()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Chưa thanh toán"
            //Xóa session
            return View();
        }

        public IActionResult CheckoutSuccess()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Paypal" và thành công
            //Xóa session
            return View();
        }
    }
}
