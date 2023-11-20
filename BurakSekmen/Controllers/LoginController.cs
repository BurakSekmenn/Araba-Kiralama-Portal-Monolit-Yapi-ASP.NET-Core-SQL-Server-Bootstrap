using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;

namespace BurakSekmen.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;

        public LoginController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService, AppDbContext appDbContext, IFileProvider fileProvider = null)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _appDbContext = appDbContext;
            _fileProvider = fileProvider;
            
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if(_appDbContext.Users.Count(s=>s.UserName == model.UserName) > 0)
            {
                _notyfService.Error("Girilen Kullanıcı Adı Kayıtlıdır!");
                return View(model);
            }
            if(_appDbContext.Users.Count(s=>s.Email == model.Email) > 0) {
                _notyfService.Error("Girilen E-Posta Adresi Kayıtlıdır!");
                return View(model);
            }

            var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.PhotoFile.Length>0 && model.PhotoFile !=null)
            {
                var filename = Guid.NewGuid().ToString()+Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootFolder.First(x => x.Name == "Photos").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;
            }

            var hashedpass = MD5Hash(model.Password);
            var user = new User();
            user.FullName = model.FullName; 
            user.UserName = model.UserName;
            user.Password = hashedpass;
            user.Email = model.Email;
            user.PhotoUrl = photoUrl;
            user.Role = "Personel";
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            _notyfService.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");
            return RedirectToAction("Index");
        }
        public string MD5Hash(string pass)
        {
            var salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Index(LoginModel model, string returnUrl=null)
        {
            var hashedpass = MD5Hash(model.Password);
            var user = _appDbContext.Users.Where(s => s.UserName == model.UserName && s.Password == hashedpass).SingleOrDefault();
            if (user == null)
            {
                _notyfService.Error("Kullanıcı Adı veya Parola Geçersizdir.");
                return View();
            }


            List<Claim> claims = new List<Claim>()
            {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("UserName",user.UserName),
            new Claim("FullName",user.FullName),
            new Claim("PhotoUrl",user.PhotoUrl)
           
            };

            ClaimsIdentity ıdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(ıdentity);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepMe
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            return RedirectToAction("Index", "Admin");
        }
    }
}
