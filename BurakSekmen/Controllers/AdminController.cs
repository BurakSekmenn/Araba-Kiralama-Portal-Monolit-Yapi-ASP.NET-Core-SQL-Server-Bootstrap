using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurakSekmen.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        public AdminController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notyfService, AppDbContext appDbContext = null)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            ViewBag.toplam= _appDbContext.Vehicles.Count().ToString();
            ViewBag.kategori=_appDbContext.AracKategoris.Count().ToString();
            ViewBag.yakıt=_appDbContext.AracYaks.Count().ToString();
            ViewBag.marka=_appDbContext.AracMarkas.Count().ToString();



            var araclarım = _appDbContext.Vehicles
                  .Include(x => x.AracKategori)
                  .Include(x => x.aracYakıt)
                  .OrderByDescending(x => x.Id)
                  .Take(5)
                    .Select(x => new VehicleViewModel()
                    {
                        AracAdı = x.AracAdı,
                        AracKategoriTurü = x.AracKategori != null ? x.AracKategori.AracKategoriAdi : "Belirtilmemiş",
                        AracYakıtTuru = x.aracYakıt != null ? x.aracYakıt.AracYakıtTuru : "Belirtilmemiş",
                        ResimData = x.Resim,
                        Arackm = x.Arackm,
                        Fiyat = x.Fiyat,
                        Id = x.Id,
                    })
                     .ToList();
            return View(araclarım);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Role()
        {
            var role = _appDbContext.Users.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Role = x.Role
            }).ToList();
           return View  (role);
        }
        [Authorize(Roles = "admin")]
        public JsonResult GetRole()
        {
            var roles = _appDbContext.Users
            .Select(x => new RoleViewModel
             {
            Id = x.Id,
            FullName = x.FullName,
            UserName = x.UserName,
            Role = x.Role
             })
             .ToList();
            return Json (roles);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("/Admin/Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var user = _appDbContext.Users
                        .Where(x => x.Id == id)
                        .Select(x => new RoleViewModel
                             {
                                 Id = x.Id,
                                 FullName = x.FullName,
                                 UserName = x.UserName,
                                 Role = x.Role
                              }).FirstOrDefault();
           
            return View(user);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Edit(RoleViewModel roleViewModel)
        {

            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == roleViewModel.Id);
            user.FullName = roleViewModel.FullName;
            user.UserName = roleViewModel.UserName;
            user.Role = roleViewModel.Role;
            _appDbContext.SaveChanges();
            _notyfService.Success("Kayıt Başarılı bir Şekilde Güncellendi");
            return RedirectToAction("Role");
        }


       
        [Authorize(Roles = "admin")]
        public IActionResult SiteSeo(SiteSeoViewModel model) {
          
            var siteSeo = _appDbContext.Siteseos
                 .Where(x => x.Id == 1)
                 .Select(x => new SiteSeoViewModel
                 {
                     sitebasligi=x.sitebasligi,
                     aciklama =x.aciklama,
                     siteanahtarkelime =x.siteanahtarkelime,
                     sitelogo =x.sitelogo,
                     hakkimizda=x.hakkimizda,
                     Id=x.Id,
                 }).FirstOrDefault();
            return View(siteSeo);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult SiteSeoUpdate(SiteSeoViewModel model)
        {
            try
            {
                var product = _appDbContext.Siteseos.SingleOrDefault(x => x.Id == 1);

                if (product != null)
                {
                    product.sitebasligi = model.sitebasligi;
                    product.aciklama = model.aciklama;
                    product.siteanahtarkelime = model.siteanahtarkelime;
                    product.sitelogo = model.sitelogo;
                    product.hakkimizda = model.hakkimizda;

                    _appDbContext.Siteseos.Update(product);
                    _appDbContext.SaveChanges();

                    return Json(new { success = true, message = "Kayıt başarıyla güncellendi." });
                }
                else
                {
                    return Json(new { success = false, message = "Kayıt bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya başka bir işlem yapabilirsiniz
                return Json(new { success = false, message = "Güncelleme sırasında bir hata oluştu." });
            }
        }
     
    }
}
