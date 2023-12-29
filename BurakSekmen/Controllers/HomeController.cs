using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Extensions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
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
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager = null, IFileProvider fileProvider = null, AppDbContext appDbContext = null)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _fileProvider = fileProvider;
            _appDbContext = appDbContext;
        }
        private string userId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task userImage()
        {
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.UserProfile = user!.PhotoUrl;
            ViewBag.FullName = user!.FullName;
        }

            public IActionResult Index()
        {
            List<int> istenenIds = new List<int> { 5, 2, 3, 4, 1 };

            var veri = _appDbContext.Vehicles
                .Include(ay => ay.aracYakıt)
                .Include(ak => ak.AracKategori)
                .Include(at => at.aracMarka)
                .Where(x => istenenIds.Contains(x.Id))
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    AracAdı = x.AracAdı,
                    AracKategoriTurü = x.AracKategori != null ? x.AracKategori.AracKategoriAdi : "Belirtilmemiş",
                    AracYakıtTuru = x.aracYakıt != null ? x.aracYakıt.AracYakıtTuru : "Belirtilmemiş",
                    Marka = x.aracMarka != null ? x.aracMarka.aracmarka : "Belirtilmemiş",
                    ResimData = x.Resim,
                    AracValizSayisi = x.AracValizSayisi,
                    Fiyat = x.Fiyat,
                    AracKoltukSayisi = x.AracKoltukSayisi,


                }).Take(5).ToList();
            return View(veri);


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
            if(model.PhotoFile == null)
            {
                _notyfService.Information("Lütfen Fotoğraf Yükleyiniz");
                return View();
            }
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
        public async Task<IActionResult> Dashboard()
        {
            await userImage();
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Dashboard(PasswordChangeViwModel request)
        {
           
            var currentuser =await _userManager.FindByNameAsync(User.Identity!.Name!);

            var checkoldPasword =await _userManager.CheckPasswordAsync(currentuser, request.PaswordOld);

            if (!checkoldPasword)
            {
                ModelState.AddModelError(string.Empty, "Eski Şifreniz Yanlış");
                _notyfService.Warning("Eski Şifreniz Yanlış");
                return View();
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentuser, request.PaswordOld,request.PaswordNew);


            if (!resultChangePassword.Succeeded)
            {
                ModelState.AddModelErroList(resultChangePassword.Errors.Select(x => x.Description).ToList());
                _notyfService.Error("Yeni şifre ve yeni şifre tekrarı birbirini aynısı olmalıdır.");
                return RedirectToAction("Dashboard", "Home");
            }



            await _userManager.UpdateSecurityStampAsync(currentuser);
           

            _notyfService.Success("Şifre Değişitirme İşlemi Başarılı Bir Şekilde Gerçekleştirildi");

            return RedirectToAction("Dashboard", "Home");
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
        [HttpGet]
        public IActionResult ArabaDetay(int ıd)
        {
            var araclarım = _appDbContext.Vehicles
                 .Where(x => x.Id == ıd)
                 .Include(ay => ay.aracYakıt)
                 .Include(ak => ak.AracKategori)
                 .Include(at => at.aracMarka)
                 .Select(x => new VehicleViewModel
                 {
                     Id = x.Id,
                     AracKategoriTurü = x.AracKategori != null ? x.AracKategori.AracKategoriAdi : "Belirtilmemiş",
                     AracYakıtTuru = x.aracYakıt != null ? x.aracYakıt.AracYakıtTuru : "Belirtilmemiş",
                     Marka = x.aracMarka != null ? x.aracMarka.aracmarka : "Belirtilmemiş",
                     Arackm = x.Arackm,
                     AracMotorTip= x.AracMotorTip,
                     Klima = x.Klima,
                     SesGirisi = x.SesGirisi,
                     ilkyardımcantası = x.ilkyardımcantası,
                     bluetooth = x.bluetooth,
                     klimakontrol = x.klimakontrol,
                     bagaj = x.bagaj,
                     ResimData = x.Resim,
                     Fiyat = x.Fiyat,
                     AracAcıklama = x.AracAcıklama,
                     AracAdı = x.AracAdı,
                 }).ToList();
            ViewBag.AracAdı = _appDbContext.Vehicles
                              .Where(x => x.Id == ıd)
                              .Select(y => y.AracAdı)
    .                         FirstOrDefault();
            return View(araclarım);
        }

        public IActionResult Privacy()
        {
            //userImage();
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            var iletisim = new Contact();
            iletisim.Email = model.Email;
            iletisim.Phone = model.Phone;
            iletisim.NameandSurname = model.NameandSurname;
            iletisim.Message = model.Message;
            
            _appDbContext.Contacts.Add(iletisim);
            await _appDbContext.SaveChangesAsync();
            _notyfService.Success("Başarılı Bir Şekilde Mesajınız iletilmiştir.",2);
            return RedirectToAction("Contact", "Home");
          
        }



        public IActionResult MyCars()
        {
            var veri = _appDbContext.Vehicles
                .Include(ay => ay.aracYakıt)
                .Include(ak => ak.AracKategori)
                .Include(at => at.aracMarka)
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    AracAdı = x.AracAdı,
                    AracKategoriTurü = x.AracKategori != null ? x.AracKategori.AracKategoriAdi : "Belirtilmemiş",
                    AracYakıtTuru = x.aracYakıt != null ? x.aracYakıt.AracYakıtTuru : "Belirtilmemiş",
                    Marka = x.aracMarka != null ? x.aracMarka.aracmarka : "Belirtilmemiş",
                    ResimData = x.Resim,
                    AracValizSayisi = x.AracValizSayisi,
                    Fiyat = x.Fiyat,
                    AracKoltukSayisi = x.AracKoltukSayisi,


                }).ToList();

            return View(veri);
        }

        public async Task<IActionResult> Notlarım()
        {
            await userImage();
            return View();
        }
        public async Task<IActionResult> NotBul()
        {
            var user = await _userManager.FindByIdAsync(userId);
            var username = user!.UserName;
            await userImage();
            
            var notGetir = _appDbContext.UserNots.Where(x=>x.UserName == username).Select(x => new UserNotViewModel()
            {
                Id = x.Id,
                Not = x.Not,
            }).ToList();
            return Json(notGetir);
        }


        [HttpPost]
        public async Task<IActionResult> NotUpdate(UserNotViewModel model)
        {
            await userImage();
            var nots = _appDbContext.UserNots.SingleOrDefault(x => x.Id == model.Id);
            nots!.Not = model.Not;
            _appDbContext.UserNots.Update(nots!);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }



        [HttpPost]
        public async Task<IActionResult> NotDelete(UserNotViewModel model)
        {
            await userImage();
            var deletekayıt = _appDbContext.UserNots.FirstOrDefault(x => x.Id == model.Id);
            _appDbContext.UserNots.Remove(deletekayıt!);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });

        }
        [HttpPost]
        public async Task<IActionResult> NotInsert(UserNotViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var username = user!.UserName;
            await userImage();
            var notekle = new UserNot();
            notekle.Not = model.Not;
            notekle.UserName = username!;
            _appDbContext.UserNots.Add(notekle);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }



        public IActionResult AccessDenied()
        {
            // Access denied action implementation
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[Route("/StatusCodeError/{statusCode}")]
        //public IActionResult Error(int statusCode)
        //{
        //    if (statusCode == 404)
        //    {
        //        ViewBag.ErrorMessage = "404 Page";
        //    }

        //    return View();
        //}


    }
}