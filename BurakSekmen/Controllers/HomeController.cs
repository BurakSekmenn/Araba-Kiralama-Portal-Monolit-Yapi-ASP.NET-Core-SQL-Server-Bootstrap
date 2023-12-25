using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BurakSekmen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService, UserManager<User> userManager)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _userManager = userManager;
        }
        private string userId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async void userImage()
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserProfile = user!.PhotoUrl;

        }


        public IActionResult Index()
        {

            userImage();
            return View();
        }

        public IActionResult Privacy()
        {
            userImage();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}