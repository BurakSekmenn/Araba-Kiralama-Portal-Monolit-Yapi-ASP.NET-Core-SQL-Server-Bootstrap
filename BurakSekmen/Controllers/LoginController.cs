using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Extensions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NETCore.Encrypt.Extensions;
using System.Data;
using System.Security.Claims;

namespace BurakSekmen.Controllers
{
    [Authorize(Roles = "admin,calisan")]
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService, AppDbContext appDbContext, IFileProvider fileProvider = null, UserManager<User> userManager = null, RoleManager<Role> roleManager = null, SignInManager<User> signInManager = null)
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
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.PhotoFile.Length > 0 && model.PhotoFile != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootFolder.First(x => x.Name == "Photos").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;
            }



            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var identityResult = await _userManager.CreateAsync(new() { UserName = model.UserName,Email = model.Email,PhotoUrl = photoUrl,FullName = model.FullName},model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("calisan");
            if (!roleExist)
            {
                var role = new Role { Name = "calisan" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "calisan");
            _notyfService.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");
            return RedirectToAction("Index","Login");
         
        }
        public string MD5Hash(string pass)
        {
            var salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }


        public async Task<IActionResult> AccessDenied()
        {
          
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async  Task<IActionResult> Index(LoginModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Admin");

            var hasUser = await _userManager.FindByNameAsync(model.UserName);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email Veya Şifre Yanlış");
                _notyfService.Warning("Email Veya Şifrenizi Yanlış Girdiniz.");
                return View();
            }

            if (!await _userManager.IsInRoleAsync(hasUser, "admin") && !await _userManager.IsInRoleAsync(hasUser, "calisan"))
            {
                ModelState.AddModelError(string.Empty, "Email Veya Şifre Yanlış");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.KeepMe, true);


            if (result.Succeeded)
            {
                _notyfService.Success("Giriş Başarılı");
                return Redirect(returnUrl);
                
            }


            ModelState.AddModelErroList(new List<string>()
            {
                "Email Veya Şifreniniz Yanlış"
            });



            if (result.IsLockedOut)
            {
                ModelState.AddModelErroList(new List<string>() { $"3 dakika boyunca giriş yapamazsınız." });
                return View();
            }

            ModelState.AddModelErroList(new List<string>() { $"Email veya şifre yanlış(Başarısız Giriş Sayısı)", $"Başarısız Giriş Sayısı={await _userManager.GetAccessFailedCountAsync(hasUser)}" });



            _notyfService.Warning("Hatalı Giriş");
            return View();

        }
    }
}
