using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
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
            var kayıtgetir = _appDbContext.AracYaks.Select(x => new VehicleAracKayıtViewModel()
            {
                Id = x.Id,
                AracYakıtTuru = x.AracYakıtTuru,
            }).ToList();
            return View(kayıtgetir);
        }


    }
}
