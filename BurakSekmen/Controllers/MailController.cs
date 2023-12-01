using AspNetCoreHero.ToastNotification.Abstractions;
using BurakSekmen.Models;
using BurakSekmen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace BurakSekmen.Controllers
{
    public class MailController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;
        public MailController(ILogger<HomeController> logger = null, IConfiguration configuration = null, INotyfService notyfService = null, AppDbContext appDbContext = null, IFileProvider fileProvider = null)
        {
            _logger = logger;
            _configuration = configuration;
            _notyfService = notyfService;
            _appDbContext = appDbContext;
            _fileProvider = fileProvider;
        }
        public IActionResult Index()
        {
            var email = @User.FindFirst("Email").Value;
            var bul = _appDbContext.Mails.Where(x => x.Alici == email)
                .Select(x => new MailViewModel()
                {
                    icerik = x.icerik,
                    Gonderici = x.Gonderici,
                    Konu = x.Konu,
                    Id = x.Id,
                    Alici = x.Alici,
                }).ToList();
            return View(bul);
        }
        public IActionResult GonderilenMail()
        {
            var email = User.FindFirst("Email").Value;
            var bul = _appDbContext.Mails.Where(x => x.Gonderici == email)
                .Select(x => new MailViewModel()
                {
                    icerik = x.icerik,
                    Gonderici = x.Gonderici,
                    Konu = x.Konu,
                    Id = x.Id,
                    Alici = x.Alici,
                }).ToList();
            return View(bul);
        }

        public IActionResult SendMail(int id)
        {
            var sendmail = _appDbContext.Mails
                .Where(x=>x.Id == id)
                .Select(x => new MailViewModel()
                {
                    Alici = x.Alici,
                    Gonderici=x.Gonderici,
                    Konu = x.Konu,
                    Id = x.Id,
                    icerik=x.icerik,
                }).ToList();


            return View(sendmail);
        }


        [HttpGet]
        public IActionResult MailGonder()
        {
            List<SelectListItem> kisileribul = (from x in _appDbContext.Users.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.FullName,
                                                   Value = x.Email,
                                               }).ToList();
            ViewBag.dgr1 = kisileribul;

            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> MailGonder(MailViewModel model)
        {
            var mail = @User.FindFirst("Email").Value;
            var kaydet = new Mail();
            kaydet.Gonderici =mail;
            kaydet.Alici = model.Alici;
            kaydet.Konu = model.Konu;
            kaydet.icerik = model.icerik;
            _appDbContext.Mails.Add(kaydet);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
