using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BurakSekmen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,INotyfService notyfService)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}