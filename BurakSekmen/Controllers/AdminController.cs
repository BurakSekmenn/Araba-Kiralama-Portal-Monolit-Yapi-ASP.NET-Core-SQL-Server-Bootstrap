using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurakSekmen.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        public AdminController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
        }
    
        public IActionResult Index()
        {
            return View();
        }
     
    }
}
