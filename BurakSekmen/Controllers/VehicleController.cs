using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurakSekmen.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        public VehicleController(ILogger<HomeController> logger, IConfiguration configuration, AppDbContext appDbContext, INotyfService notyfService)
        {
            _logger = logger;
            _configuration = configuration;
            _appDbContext = appDbContext;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VehicleCategory()
        {
            return View();
        }
    }
}
