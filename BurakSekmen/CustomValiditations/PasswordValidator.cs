using BurakSekmen.Models;
using Microsoft.AspNetCore.Identity;

namespace BurakSekmen.CustomValiditations
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
            var erros = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                erros.Add(new()
                {
                    Code = "PaswordContainUserName",
                    Description = "Şifre Alanı Kullanıcı Adı İçeremez"

                });
            }
            if (password!.ToLower().StartsWith("123"))
            {
                erros.Add(new()
                {
                    Code = "PaswordContain123",
                    Description = "Şifre Alanı ardaşık sayı içeremez"

                });
            }
            if (password!.StartsWith("abc"))
            {
                erros.Add(new()
                {
                    Code = "PaswordContainabc",
                    Description = "Şifre Alanı abc olarak başlayamaz"
                });
            }

            if (erros.Any())
            {
                return Task.FromResult(IdentityResult.Failed(erros.ToArray()));
            }


            return Task.FromResult(IdentityResult.Success);





        }
    }
}
