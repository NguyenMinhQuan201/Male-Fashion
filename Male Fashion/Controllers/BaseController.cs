using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RazorWeb.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers["REMOTE_ADDR"].ToString();


            base.OnActionExecuting(context);
        }
        public string ip()
        {
            string ipAddress;
            try
            {
                ipAddress = HttpContext.Request.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString();

                if (string.IsNullOrEmpty(ipAddress) || ipAddress.ToLower() == "unknown")
                    ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }
            return ipAddress;
        }
    }
}
