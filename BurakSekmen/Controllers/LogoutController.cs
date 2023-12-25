using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BurakSekmen.Controllers
{
    public class LogoutController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
      
    }
}
