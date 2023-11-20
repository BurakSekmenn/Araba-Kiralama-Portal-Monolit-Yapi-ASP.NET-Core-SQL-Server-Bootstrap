using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
