using System.ComponentModel.DataAnnotations;

namespace BurakSekmen.ViewModels
{
    public class ContactViewModel
    {
        
        [Required(ErrorMessage = "Adınız Ve Soyadınız Giriniz!")]
        public string NameandSurname { get; set; }
        [Required(ErrorMessage = "Email Giriniz!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefon Numrasını Giriniz!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Messajınızı Giriniz!")]
        public string Message { get; set; }

    }
}
