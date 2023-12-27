using System.ComponentModel.DataAnnotations;

namespace BurakSekmen.ViewModels
{
    public class PasswordChangeViwModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Eski Şifre Alanı Boş Bırakılamaz.")]
        [MinLength(6,ErrorMessage ="Şifreniz En az 6 Karakter Olabilir.")]
        public string PaswordOld { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre Alanı Boş Bırakılamaz.")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 Karakter Olabilir.")]
        public string PaswordNew { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre Tekrar Alanı Boş Bırakılamaz.")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 Karakter Olabilir.")]
        public string PaswordConfirm{ get; set; } = null!;

        public string userName { get; set; }

        public string Email { get; set; }


    }
}
