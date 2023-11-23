using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace BurakSekmen.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;

        public VehicleController(ILogger<HomeController> logger, IConfiguration configuration, AppDbContext appDbContext, INotyfService notyfService, IFileProvider fileProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _appDbContext = appDbContext;
            _notyfService = notyfService;
            _fileProvider = fileProvider;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VehicleCategory()
        {
            return View();
        }
        [HttpGet]
        public IActionResult VehicleYakıt()
        {
            var kayıtgetir = _appDbContext.AracYaks.Select(x => new VehicleAracKayıtViewModel()
            {
                Id = x.Id,
                AracYakıtTuru = x.AracYakıtTuru,
            }).ToList();
            return View(kayıtgetir);
        }

        [HttpPost]
        public IActionResult VehiclYakıtKaydet(VehicleAracKayıtViewModel model)
        {
            var aracyakıt = new AracYakıt();
            aracyakıt.AracYakıtTuru = model.AracYakıtTuru;
            _appDbContext.AracYaks.Add(aracyakıt);
            _appDbContext.SaveChanges();
            return RedirectToAction("VehicleYakıt", "Vehicle");
        }
        [HttpPost]
        public IActionResult VehiclYakıtDelete(VehicleAracKayıtViewModel model)
        {
            var delete = _appDbContext.AracYaks.SingleOrDefault(x => x.Id == model.Id);
            _appDbContext.AracYaks.Remove(delete);
            _appDbContext.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult VehiclYakıtUpdate(VehicleAracKayıtViewModel model)
        {
            var guncelle = _appDbContext.AracYaks.SingleOrDefault(x => x.Id == model.Id);
            guncelle.AracYakıtTuru = model.AracYakıtTuru;
            _appDbContext.AracYaks.Update(guncelle);
            _appDbContext.SaveChanges();
            return Json(new { success = true });
        }
        public IActionResult GetUpdatedTableData()
        {
            var kayıtgetir = _appDbContext.AracYaks.Select(x => new VehicleAracKayıtViewModel()
            {
                Id = x.Id,
                AracYakıtTuru = x.AracYakıtTuru,
            }).ToList();
          
            return Json(kayıtgetir);
        }


        [HttpGet]
        public IActionResult VehicleKategori()
        {
            var kayıtgetir = _appDbContext.AracKategoris.Select(x => new VehicleKategoriKayitModel()
            {
                Id = x.Id,
                AracKategoriAdi =x.AracKategoriAdi
            }).ToList();
            return View(kayıtgetir);
        }
        [HttpPost]
        public IActionResult VehicleKategoriKaydet(VehicleKategoriKayitModel model)
        {
            var aracKategori = new AracKategori();
            aracKategori.AracKategoriAdi = model.AracKategoriAdi;
            _appDbContext.AracKategoris.Add(aracKategori);
            _appDbContext.SaveChanges();
            return RedirectToAction("VehicleKategori", "Vehicle");
        }
        [HttpPost]
        public IActionResult VehicleKategoriUpdate(VehicleKategoriKayitModel model)
        {
            var guncelleveri = _appDbContext.AracKategoris.SingleOrDefault(x => x.Id == model.Id);
            guncelleveri.AracKategoriAdi = model.AracKategoriAdi;
            _appDbContext.AracKategoris.Update(guncelleveri);
            _appDbContext.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult VehicleKategoriDelete(VehicleKategoriKayitModel model)
        {
            var deleteveri = _appDbContext.AracKategoris.SingleOrDefault(x => x.Id == model.Id);
            _appDbContext.AracKategoris.Remove(deleteveri);
            _appDbContext.SaveChanges();
            return Json(new { success = true });
        }

        public IActionResult Vehicle()
        {
            var araclarım = _appDbContext.Vehicles
                 .Include(x => x.AracKategori)  // AracKategori tablosunu yükleyin
                 .Include(x => x.aracYakıt)     // AracYakıt tablosunu yükleyin
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
                    
                })
                    .ToList();
            return View(araclarım);
        }

        public IActionResult VehicleInsert()
        {
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

            // burayı böyle mi getirlmeli yoksa başka bir yoldan mı ben buradan verileri alıyorum direk yada sen ne kullanıyorsun
            // ilişkili bir tablo
            return View();
  
        }
        [HttpPost]
        public IActionResult VehicleInsert(VehicleViewModel model)
        {
            var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if(model.Resim.Length>0 && model.Resim != null)
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
            _appDbContext.SaveChanges();
            return RedirectToAction("Vehicle","Vehicle");
        }

    }
}
