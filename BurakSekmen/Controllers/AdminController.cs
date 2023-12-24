using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System;

namespace BurakSekmen.Controllers
{
    [Authorize]
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

            var latestVehicles = _appDbContext.Vehicles
             .Include(ay => ay.aracYakıt) // AracYakit tablosunu Arac ile birleştir
             .Include(ak => ak.AracKategori) // AracYakit tablosunu AracKategori ile birleştir
             .Include(at =>at.aracMarka)
             .OrderByDescending(ay => ay.Id) // Sıralama kriterini belirt
             .Take(5)
             .ToList();



            VehicleAndDuyuruViewModel viewModel = new VehicleAndDuyuruViewModel
            {
                Vehicles = latestVehicles,
                Duyuru = _appDbContext.Duyurs.Where(d => d.Durum == true).OrderByDescending(dy=>dy.Id).Take(3).ToList(),
                Users = _appDbContext.Users.ToList(),
            };

            return View(viewModel);

           
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


        [Authorize(Roles = "admin")]
        public IActionResult Duyuru()
        {
            var duyurugetir = _appDbContext.Duyurs.Select(x => new DuyuruViewModel()
            {
                Id = x.Id,
                DuyurAcıklama=x.DuyurAcıklama,
                Durum=x.Durum
                
            }).ToList();
            return View(duyurugetir);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DuyurEkle(DuyuruViewModel model)
        {
            var duyurukayıt = new Duyuru();
            duyurukayıt.DuyurAcıklama = model.DuyurAcıklama;
            duyurukayıt.Durum = model.Durum;
            _appDbContext.Duyurs.Add(duyurukayıt);
            await _appDbContext.SaveChangesAsync();
            _notyfService.Success("Kayıt Ekleme Başarılı");
            return RedirectToAction("Duyuru", "Admin");
        }
        [Authorize(Roles = "admin")]
        public IActionResult BadgeUptade(bool Durum,DuyuruViewModel model)
            {
            bool yeniDurum = Durum;
            var update = _appDbContext.Duyurs.SingleOrDefault(x => x.Id == model.Id);
            update.Durum = yeniDurum;
            _appDbContext.Duyurs.Update(update);
            _appDbContext.SaveChanges();
            _notyfService.Success("Duyuru Durumu Güncellendi");
            return RedirectToAction("Duyuru", "Admin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult DuyurGuncelle(int id)
        {
            var kayıtgetir = _appDbContext.Duyurs
             .Where(x => x.Id == id)
              .Select(x => new DuyuruViewModel()
                {
                    Id = x.Id,
                    DuyurAcıklama = x.DuyurAcıklama,
                    Durum = x.Durum,
                 })
                 .SingleOrDefault(); 

            return View(kayıtgetir);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DuyurGuncelle(DuyuruViewModel model)
        {
            var guncelle = _appDbContext.Duyurs.SingleOrDefault(x => x.Id == model.Id);
            guncelle.DuyurAcıklama = model.DuyurAcıklama;
            guncelle.Durum=model.Durum;
            _appDbContext.Duyurs.Update(guncelle);
            _appDbContext.SaveChanges();
            _notyfService.Success("Kayıt Güncelleme Başarılı");
            return RedirectToAction("Duyuru", "Admin");
        }
        [Authorize(Roles = "admin")]
        public IActionResult DuyuruSil(int id)
        {
            var guncelle = _appDbContext.Duyurs.SingleOrDefault(x => x.Id == id);
            _appDbContext.Duyurs.Remove(guncelle!);
            _appDbContext.SaveChanges();    

            _notyfService.Success("Kayıt Silme Başarılı");
            return RedirectToAction("Duyuru", "Admin");

        }
        public IActionResult Deneme()
        {
            return View();
        }



    }
}
