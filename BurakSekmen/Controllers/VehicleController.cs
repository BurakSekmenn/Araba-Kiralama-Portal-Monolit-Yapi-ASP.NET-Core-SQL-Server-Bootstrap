﻿using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Security.Claims;

namespace BurakSekmen.Controllers
{
    [Authorize(Roles = "admin,calisan")]
    public class VehicleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public VehicleController(ILogger<HomeController> logger, IConfiguration configuration, AppDbContext appDbContext, INotyfService notyfService, IFileProvider fileProvider, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _configuration = configuration;
            _appDbContext = appDbContext;
            _notyfService = notyfService;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        private string userId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public async Task userImage()
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserProfile = user!.PhotoUrl;
     
        }


        public async Task<IActionResult> Index()
        {
            await userImage();
            return View();
        }
        public async Task<IActionResult> VehicleCategory()
        {
            await userImage();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> VehicleYakıt()
        {
            await userImage();
            var kayıtgetir = _appDbContext.AracYaks.Select(x => new VehicleAracKayıtViewModel()
            {
                Id = x.Id,
                AracYakıtTuru = x.AracYakıtTuru,
            }).ToList();
            return View(kayıtgetir);
        }

        [HttpPost]
        public async Task<IActionResult> VehiclYakıtKaydet(VehicleAracKayıtViewModel model)
        {
            await userImage();
            var aracyakıt = new AracYakıt();
            aracyakıt.AracYakıtTuru = model.AracYakıtTuru;
            await _appDbContext.AracYaks.AddAsync(aracyakıt);
            await _appDbContext.SaveChangesAsync();
            _notyfService.Success("Kayıt Başarılı Bir Şekilde Gerçekleştirildi");
            return RedirectToAction("VehicleYakıt", "Vehicle");
        }
        [HttpPost]
        public async Task<IActionResult> VehiclYakıtDelete(VehicleAracKayıtViewModel model)
        {
            await userImage();
            var deletekayıt = _appDbContext.AracYaks.FirstOrDefault(x => x.Id == model.Id);
            _appDbContext.AracYaks.Remove(deletekayıt!);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> VehiclYakıtUpdate(VehicleAracKayıtViewModel model)
        {
            await userImage();
            var guncelle = _appDbContext.AracYaks.SingleOrDefault(x => x.Id == model.Id);
            guncelle.AracYakıtTuru = model.AracYakıtTuru;
            _appDbContext.AracYaks.Update(guncelle);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }

        public async Task<IActionResult> vehiclegetir()
        {
            await userImage();

            var kayıtgetir = _appDbContext.AracKategoris.Select(x => new VehicleKategoriKayitModel()
            {
                Id = x.Id,
                AracKategoriAdi = x.AracKategoriAdi
            }).ToList();
            return Json(kayıtgetir);
        }
        public async Task<IActionResult> GetUpdatedTableData()
        {
            await userImage();
            var kayıtgetir = _appDbContext.AracYaks.Select(x => new VehicleAracKayıtViewModel()
            {
                Id = x.Id,
                AracYakıtTuru = x.AracYakıtTuru,
            }).ToList();

            return Json(kayıtgetir);
        }


        [HttpGet]
        public async Task<IActionResult> VehicleKategori()
        {
            await userImage();
            var kayıtgetir = _appDbContext.AracKategoris.Select(x => new VehicleKategoriKayitModel()
            {
                Id = x.Id,
                AracKategoriAdi = x.AracKategoriAdi
            }).ToList();
            return View(kayıtgetir);
        }
        [HttpPost]
        public async Task<IActionResult> VehicleKategoriKaydet(VehicleKategoriKayitModel model)
        {
            await userImage();
            var aracKategori = new AracKategori();
            aracKategori.AracKategoriAdi = model.AracKategoriAdi;
            _appDbContext.AracKategoris.Add(aracKategori);
            await _appDbContext.SaveChangesAsync();
            _notyfService.Success("Yeni Araç Tipi Eklendi");
            return RedirectToAction("VehicleKategori", "Vehicle");
        }
        [HttpPost]
        public async Task<IActionResult> VehicleKategoriUpdate(VehicleKategoriKayitModel model)
        {
            await userImage();
            var guncelleveri = _appDbContext.AracKategoris.SingleOrDefault(x => x.Id == model.Id);
            guncelleveri.AracKategoriAdi = model.AracKategoriAdi;
            _appDbContext.AracKategoris.Update(guncelleveri);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> VehicleKategoriDelete(VehicleKategoriKayitModel model)
        {
            await userImage();
            var deleteveri = _appDbContext.AracKategoris.FirstOrDefault(x => x.Id == model.Id);
            if (deleteveri != null)
            {
                _appDbContext.AracKategoris.Remove(deleteveri);
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Kategori bulunamadı." });
        }
        [HttpGet]
        public async Task<IActionResult> KategoriArac(int id)
        {
            await userImage();
            var araclarım = _appDbContext.Vehicles
            .Include(x => x.AracKategori)
            .Include(x => x.aracYakıt)
            .Where(x => x.AracKategorId == id)
            .Select(x => new VehicleViewModel()
            {
                AracAdı = x.AracAdı,
                AracKategoriTurü = x.AracKategori != null ? x.AracKategori.AracKategoriAdi : "Belirtilmemiş",
                AracYakıtTuru = x.aracYakıt != null ? x.aracYakıt.AracYakıtTuru : "Belirtilmemiş",
                Arackm = x.Arackm,
                AracMotorTip = x.AracMotorTip,
                AracKoltukSayisi = x.AracKoltukSayisi,
                AracValizSayisi = x.AracValizSayisi,
                AracAcıklama = x.AracAcıklama,
                ResimData = x.Resim,
                Id = x.Id,
            })
                .ToList();
            if (araclarım.Count > 0)
            {
                _notyfService.Success("Kategoriye Göre Araçlar Sıralandı");
                return View(araclarım);
            }
            else
            {
                _notyfService.Warning("Belirtilen kategoriye ait araç bulunamadı");
                return View(araclarım);
            }
        }

        public async Task<IActionResult> Vehicle()
        {
            await userImage();
            var araclarım = _appDbContext.Vehicles
                 .Include(x => x.AracKategori)
                 .Include(x => x.aracYakıt)
                   .Select(x => new VehicleViewModel()
                   {
                       AracAdı = x.AracAdı,
                       AracKategoriTurü = x.AracKategori != null ? x.AracKategori.AracKategoriAdi : "Belirtilmemiş",
                       AracYakıtTuru = x.aracYakıt != null ? x.aracYakıt.AracYakıtTuru : "Belirtilmemiş",
                       Arackm = x.Arackm,
                       AracMotorTip = x.AracMotorTip,
                       AracKoltukSayisi = x.AracKoltukSayisi,
                       AracValizSayisi = x.AracValizSayisi,
                       AracAcıklama = x.AracAcıklama,
                       ResimData = x.Resim,
                       Id = x.Id,

                   })
                    .ToList();
            return View(araclarım);
        }

        public async Task<IActionResult> VehicleInsert()
        {
            await userImage();
            List<SelectListItem> yakıtbuldu = (from x in _appDbContext.AracYaks.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AracYakıtTuru,
                                                   Value = x.Id.ToString()
                                               }).ToList();



            ViewBag.dgr1 = yakıtbuldu;
            List<SelectListItem> kategoribuldu = (from x in _appDbContext.AracKategoris.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.AracKategoriAdi,
                                                      Value = x.Id.ToString()
                                                  }).ToList();

            ViewBag.dgr2 = kategoribuldu;


            List<SelectListItem> markabul = (from x in _appDbContext.AracMarkas.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.aracmarka,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.dgr3 = markabul;


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> VehicleInsert(VehicleViewModel model)
        {
            await userImage();
            var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.Resim.Length > 0 && model.Resim != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resim.FileName);
                var photoPath = Path.Combine(rootfolder.First(x => x.Name == "Araba").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.Resim.CopyTo(stream);
                photoUrl = filename;
            }

            var kayıtyap = new Vehicle();
            kayıtyap.AracKategorId = model.AracKategorId;
            kayıtyap.AracYakıId = model.AracYakıId;
            kayıtyap.AracMarkaId = model.aracMarkaId;
            kayıtyap.Fiyat = model.Fiyat;
            kayıtyap.AracAdı = model.AracAdı;
            kayıtyap.Arackm = model.Arackm;
            kayıtyap.AracMotorTip = model.AracMotorTip;
            kayıtyap.AracKoltukSayisi = model.AracKoltukSayisi;
            kayıtyap.AracValizSayisi = model.AracValizSayisi;
            kayıtyap.AracAcıklama = model.AracAcıklama;
            kayıtyap.Resim = photoUrl;
            kayıtyap.ArabaYatagi = model.ArabaYatagi;
            kayıtyap.Arackiti = model.Arackiti;
            kayıtyap.CocukKoltugu = model.CocukKoltugu;
            kayıtyap.EmniyetKemeri = model.EmniyetKemeri;
            kayıtyap.Gps = model.Gps;
            kayıtyap.Klima = model.Klima;
            kayıtyap.Music = model.Music;
            kayıtyap.SesGirisi = model.SesGirisi;
            kayıtyap.arababilgisayarı = model.arababilgisayarı;
            kayıtyap.bagaj = model.bagaj;
            kayıtyap.bluetooth = model.bluetooth;
            kayıtyap.dolab = model.dolab;
            kayıtyap.ilkyardımcantası = model.ilkyardımcantası;
            kayıtyap.klimakontrol = model.klimakontrol;
            kayıtyap.uzaktankitleme = model.uzaktankitleme;
            _appDbContext.Add(kayıtyap);
            await _appDbContext.SaveChangesAsync();
            _notyfService.Success("Yeni Araç Başarılı Bir Şekilde Eklendi");
            return RedirectToAction("Vehicle", "Vehicle");
        }

        [HttpPost]
        public async Task<IActionResult> VehicleDelete(VehicleViewModel model)
        {
            await userImage();
            var silinecekveri = _appDbContext.Vehicles.FirstOrDefault(x => x.Id == model.Id);

            if (silinecekveri == null)
            {
                return Json(new { success = false, message = "Araç bulunamadı." });
            }

            // Aracın resim dosyasını sil
            if (!string.IsNullOrEmpty(silinecekveri.Resim))
            {
                var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
                var photoPath = Path.Combine(rootfolder.First(x => x.Name == "Araba").PhysicalPath, silinecekveri.Resim);

                if (System.IO.File.Exists(photoPath))
                {
                    System.IO.File.Delete(photoPath);
                }
            }

            _appDbContext.Vehicles.Remove(silinecekveri);
            await _appDbContext.SaveChangesAsync();


            return Json(new { success = true, message = "Araç başarıyla silindi." });
        }
        [HttpGet]
        public async Task<IActionResult> VehicleDetail(int id)
        {
            await userImage();
            List<SelectListItem> yakıtbuldu = (from x in _appDbContext.AracYaks.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AracYakıtTuru,
                                                   Value = x.Id.ToString()
                                               }).ToList();



            ViewBag.dgr1 = yakıtbuldu;
            List<SelectListItem> kategoribuldu = (from x in _appDbContext.AracKategoris.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.AracKategoriAdi,
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.dgr2 = kategoribuldu;

            List<SelectListItem> markabul = (from x in _appDbContext.AracMarkas.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.aracmarka,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.dgr3 = markabul;


            // Veritabanından aracı ID'ye göre getir
            var arac = _appDbContext.Vehicles
                .Include(x => x.AracKategori)
                .Include(x => x.aracYakıt)
                .FirstOrDefault(x => x.Id == id);






            var aracViewModel = new VehicleViewModel()
            {
                AracKategorId = arac.AracKategorId,
                AracYakıId = arac.AracYakıId,
                aracMarkaId = arac.AracMarkaId,
                Fiyat = arac.Fiyat,
                ResimData = arac.Resim,
                AracAdı = arac.AracAdı,
                Arackm = arac.Arackm,
                AracMotorTip = arac.AracMotorTip,
                AracKoltukSayisi = arac.AracKoltukSayisi,
                AracValizSayisi = arac.AracValizSayisi,
                AracAcıklama = arac.AracAcıklama,
                ArabaYatagi = arac.ArabaYatagi,
                Arackiti = arac.Arackiti,
                CocukKoltugu = arac.CocukKoltugu,
                EmniyetKemeri = arac.EmniyetKemeri,
                Gps = arac.Gps,
                Klima = arac.Klima,
                Music = arac.Music,
                SesGirisi = arac.SesGirisi,
                arababilgisayarı = arac.arababilgisayarı,
                bagaj = arac.bagaj,
                bluetooth = arac.bluetooth,
                dolab = arac.dolab,
                ilkyardımcantası = arac.ilkyardımcantası,
                klimakontrol = arac.klimakontrol,
                uzaktankitleme = arac.uzaktankitleme,
            };
            ViewBag.resim = aracViewModel.ResimData;
            return View(aracViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> VehicleDetail(VehicleViewModel model)
        {
            await userImage();

            var vehicleToUpdate = await _appDbContext.Vehicles.FindAsync(model.Id);

            if (vehicleToUpdate != null)
            {
                var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
                var photoUrl = model.ResimData?.ToString();

                if (model.Resim != null && model.Resim.Length > 0)
                {
                    // Yeni bir resim yüklendiyse
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resim.FileName);
                    var photoPath = Path.Combine(rootfolder.First(x => x.Name == "Araba").PhysicalPath, filename);

                    using var stream = new FileStream(photoPath, FileMode.Create);
                    model.Resim.CopyTo(stream);
                    photoUrl = filename;

                    // Eski resmi sil
                    if (!string.IsNullOrEmpty(vehicleToUpdate.Resim))
                    {
                        var oldPhotoPath = Path.Combine(rootfolder.First(x => x.Name == "Araba").PhysicalPath, vehicleToUpdate.Resim);
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                    }
                }
                else
                {
                    // Eğer yeni bir resim yüklenmediyse, eski resim devam etsin
                    photoUrl = vehicleToUpdate.Resim;
                }

                // Diğer özellikleri güncelle
                vehicleToUpdate.AracKategorId = model.AracKategorId;
                vehicleToUpdate.AracYakıId = model.AracYakıId;
                vehicleToUpdate.AracMarkaId = model.aracMarkaId;
                vehicleToUpdate.Fiyat = model.Fiyat;
                vehicleToUpdate.AracAdı = model.AracAdı;
                vehicleToUpdate.Arackm = model.Arackm;
                vehicleToUpdate.AracMotorTip = model.AracMotorTip;
                vehicleToUpdate.AracKoltukSayisi = model.AracKoltukSayisi;
                vehicleToUpdate.AracValizSayisi = model.AracValizSayisi;
                vehicleToUpdate.AracAcıklama = model.AracAcıklama;
                vehicleToUpdate.Resim = photoUrl;
                vehicleToUpdate.ArabaYatagi = model.ArabaYatagi;
                vehicleToUpdate.Arackiti = model.Arackiti;
                vehicleToUpdate.CocukKoltugu = model.CocukKoltugu;
                vehicleToUpdate.EmniyetKemeri = model.EmniyetKemeri;
                vehicleToUpdate.Gps = model.Gps;
                vehicleToUpdate.Klima = model.Klima;
                vehicleToUpdate.Music = model.Music;
                vehicleToUpdate.SesGirisi = model.SesGirisi;
                vehicleToUpdate.arababilgisayarı = model.arababilgisayarı;
                vehicleToUpdate.bagaj = model.bagaj;
                vehicleToUpdate.bluetooth = model.bluetooth;
                vehicleToUpdate.dolab = model.dolab;
                vehicleToUpdate.ilkyardımcantası = model.ilkyardımcantası;
                vehicleToUpdate.klimakontrol = model.klimakontrol;
                vehicleToUpdate.uzaktankitleme = model.uzaktankitleme;
                _notyfService.Success("Güncelleme Başarılı Bir Şekilde Gerçekleştirildi");
                // Değişiklikleri kaydet
                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("Vehicle", "Vehicle");
            }
            else
            {
                // ID'ye göre araç bulunamazsa
                return NotFound();
            }
        }


        public async Task<IActionResult> ArabaMarka()
        {
            await userImage();
            return View();
        }
        public async Task<IActionResult> Arababul()
        {
            await userImage();
            var arabagetir = _appDbContext.AracMarkas.Select(x => new ArabaMarkaViewModel()
            {
                Id = x.Id,
                aracmarka = x.aracmarka
            }).ToList();
            return Json(arabagetir);
        }
        [HttpPost]
        public async Task<IActionResult> ArabaUpdate(ArabaMarkaViewModel model)
        {
            await userImage();
            var marka = _appDbContext.AracMarkas.SingleOrDefault(x => x.Id == model.Id);
            marka!.aracmarka = model.aracmarka;
            _appDbContext.AracMarkas.Update(marka!);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> AracDelete(ArabaMarkaViewModel model)
        {
            await userImage();
            var deletekayıt = _appDbContext.AracMarkas.FirstOrDefault(x => x.Id == model.Id);
            _appDbContext.AracMarkas.Remove(deletekayıt!);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });

        }

        public async Task<IActionResult> AracInsert(ArabaMarkaViewModel model)
        {
            await userImage();
            var aracmarka = new AracMarka();
            aracmarka.aracmarka = model.aracmarka;
            _appDbContext.AracMarkas.Add(aracmarka);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
