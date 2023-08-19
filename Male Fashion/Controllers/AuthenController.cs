using Male_Fashion.Models;
using Male_Fashion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Male_Fashion.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IUserService _userService;
        public AuthenController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _userService.GetToken(model);
                if (token.Token == null)
                {
                    ModelState.AddModelError("", "Login fail");
                }
                else
                {
                    HttpContext.Session.SetString("Token", token.Token);
                    HttpContext.Session.SetString("UserName", model.UserName);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

    }
}
