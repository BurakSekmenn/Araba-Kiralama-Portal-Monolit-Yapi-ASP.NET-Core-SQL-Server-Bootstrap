using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Extensions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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
        private readonly IFileProvider _fileProvider;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager = null, IFileProvider fileProvider = null)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _fileProvider = fileProvider;
        }
        private string userId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async void userImage()
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserProfile = user!.PhotoUrl;
           

        }

        public IActionResult Index()
        {

            //userImage();
            return View();
        }
        public IActionResult Top()
        {
            return View();
        }


        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.PhotoFile.Length > 0 && model.PhotoFile != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootFolder.First(x => x.Name == "uyephotos").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;
            }
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var identityResult = await _userManager.CreateAsync(new() { UserName = model.UserName, Email = model.Email, PhotoUrl = photoUrl, FullName = model.FullName }, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("normaluye");
            if (!roleExist)
            {
                var role = new Role { Name = "normaluye" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "normaluye");
            _notyfService.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");
            return RedirectToAction("Index", "Home");
        }






        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByNameAsync(model.UserName);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email Veya Şifre Yanlış");
                _notyfService.Warning("Email Veya Şifrenizi Yanlış Girdiniz.");
                return View();
            }

            if (!await _userManager.IsInRoleAsync(hasUser, "normaluye"))
            {
               ModelState.AddModelError(string.Empty, "Email Veya Şifre Yanlış");
               return View();
            }
            var result = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.KeepMe, true);


            if (result.Succeeded)
            {
                _notyfService.Success("Giriş Başarılı");
                return Redirect(returnUrl!);

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

        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl!);
        }


        public IActionResult Privacy()
        {
            //userImage();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}