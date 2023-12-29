using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Data;
using System.Security.Claims;

namespace BurakSekmen.Controllers
{
    [Authorize(Roles = "admin,calisan")]
    public class MailController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public MailController(ILogger<HomeController> logger = null, IConfiguration configuration = null, INotyfService notyfService = null, AppDbContext appDbContext = null, IFileProvider fileProvider = null, UserManager<User> userManager = null, RoleManager<Role> roleManager = null, SignInManager<User> signInManager = null)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _appDbContext = appDbContext;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        private string userId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string mailadres => User.FindFirstValue(ClaimTypes.Email);
        public async Task userImage()
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserProfile = user!.PhotoUrl;
            ViewBag.EmailAdres = user!.Email;

        }
        public async Task<IActionResult> Index()
        {
            await userImage();
            var email = User.FindFirstValue(ClaimTypes.Email);
            var bul = _appDbContext.Mails.Where(x => x.Alici == email)
                .Select(x => new MailViewModel()
                {
                    icerik = x.icerik,
                    Gonderici = x.Gonderici,
                    Konu = x.Konu,
                    Id = x.Id,
                    Alici = x.Alici,
                }).ToList();
            return View(bul);
        }
        public async Task<IActionResult> GonderilenMail()
        {
            await userImage();
            var email = User.FindFirstValue(ClaimTypes.Email);
            var bul = _appDbContext.Mails.Where(x => x.Gonderici == email)
                .Select(x => new MailViewModel()
                {
                    icerik = x.icerik,
                    Gonderici = x.Gonderici,
                    Konu = x.Konu,
                    Id = x.Id,
                    Alici = x.Alici,
                }).ToList();
            return View(bul);
        }

        public async Task<IActionResult> SendMail(int id)
        {
           await userImage();
            var sendmail = _appDbContext.Mails
                .Where(x=>x.Id == id)
                .Select(x => new MailViewModel()
                {
                    Alici = x.Alici,
                    Gonderici=x.Gonderici,
                    Konu = x.Konu,
                    Id = x.Id,
                    icerik=x.icerik,
                }).ToList();


            return View(sendmail);
        }


        [HttpGet]
        public async Task<IActionResult> MailGonder()
        {
            await userImage();
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

            List<SelectListItem> kisileribul = (from user in _appDbContext.Users.ToList()
                                                where userManager.IsInRoleAsync(user, "admin").Result ||
                                                      userManager.IsInRoleAsync(user, "calisan").Result
                                                select new SelectListItem
                                                {
                                                    Text = user.FullName,
                                                    Value = user.Email,
                                                }).ToList();

            ViewBag.dgr1 = kisileribul;

            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> MailGonder(MailViewModel model)
        {
            await userImage();

            var mail = User.FindFirstValue(ClaimTypes.Email);
            var kaydet = new Mail();
            kaydet.Gonderici =mail;
            kaydet.Alici = model.Alici;
            kaydet.Konu = model.Konu;
            kaydet.icerik = model.icerik;
            _appDbContext.Mails.Add(kaydet);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
