using BurakSekmen.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurakSekmen.ViewModels
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Açıklama Giriniz:")]
        [Required(ErrorMessage = "Lütfen Seçim Yapınız !")]
        public int AracYakıId { get; set; }
        [Display(Name = "Açıklama Giriniz:")]
        [Required(ErrorMessage = "Lütfen Arac Acıklama Giriniz !")]
        public string AracAcıklama { get; set; }
        [Display(Name = "Açıklama Giriniz:")]
        [Required(ErrorMessage = "Lütfen Arac Acıklama Giriniz !")]
        public int AracKategorId { get; set; }
        public AracYakıt aracYakıt { get; set; }
        public AracKategori AracKategori { get; set; }
        [Display(Name = "Arac Adı Giriniz:")]
        [Required(ErrorMessage = "Lütfen Araç Adını Giriniz !")]
        public string AracAdı { get; set; }

        [Display(Name = "Araç Km Giriniz:")]
        [Required(ErrorMessage = "Lütfen Arac Km Giriniz !")]
        public string Arackm { get; set; }
        [Display(Name = "Araç Motor Tipini Giriniz: (Manuel/Otomatik)")]
        [Required(ErrorMessage = "Lütfen Arac Km Giriniz !")]
        public string AracMotorTip { get; set; }

        [Display(Name = "Araç Koltu Sayısını Giriniz:")]
        [Required(ErrorMessage = "Lütfen Koltu Sayısını Giriniz: !")]
        public string AracKoltukSayisi { get; set; }
        [Display(Name = "Araç Valiz Sayısını Giriniz:")]
        [Required(ErrorMessage = "Lütfen Araç Valiz Sayısını Giriniz: !")]
        public string AracValizSayisi { get; set; }

        [Display(Name = "Araba Resmi Seçiniz :")]
        [Required(ErrorMessage = "Araba Resmi Seçiniz!")]
        public IFormFile Resim { get; set; }

        [Display(Name = "Günlük Fiyatını Giriniz:")]
        [Required(ErrorMessage = "Günlük Fiyatını Giriniz: !")]
        public string Fiyat { get; set; }

        [NotMapped]
        public string ResimData { get; set; }

        public string AracKategoriTurü { get; set; }
        public string AracYakıtTuru { get; set; }

        public bool Klima { get; set; }
        public bool CocukKoltugu { get; set; }
        public bool Gps { get; set; }
        public bool bagaj { get; set; }
        public bool Music { get; set; }
        public bool EmniyetKemeri { get; set; }
        public bool ArabaYatagi { get; set; }
        public bool dolab { get; set; }
        public bool bluetooth { get; set; }
        public bool arababilgisayarı { get; set; }
        public bool SesGirisi { get; set; }
        public bool ilkyardımcantası { get; set; }
        public bool Arackiti { get; set; }
        public bool uzaktankitleme { get; set; }
        public bool klimakontrol { get; set; }
    }
}
