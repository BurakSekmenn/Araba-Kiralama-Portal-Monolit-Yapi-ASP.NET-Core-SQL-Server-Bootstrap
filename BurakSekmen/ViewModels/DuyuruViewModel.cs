using System.ComponentModel.DataAnnotations;

namespace BurakSekmen.ViewModels
{
    public class DuyuruViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Duyuru Giriniz:")]
        [Required(ErrorMessage = "Lütfen Duyuru Giriniz !")]
        public string DuyurAcıklama { get; set; }


        public bool Durum { get; set; }
    }
}
