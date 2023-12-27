using Microsoft.AspNetCore.Mvc;

namespace BurakSekmen.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
