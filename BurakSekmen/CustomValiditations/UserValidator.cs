using BurakSekmen.Models;
using Microsoft.AspNetCore.Identity;

namespace BurakSekmen.CustomValiditations
{
    public class UserValidator:IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName[0]!.ToString(), out _);

            if (isDigit)
            {
                errors.Add(new()
                {
                    Code = "UserNameContainFirstLetterDigitContainer",
                    Description = "Kullanıcı Adının ilk karatkeri sayısal bir karakter olamaz içeremez"
                });
            }


            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }


            return Task.FromResult(IdentityResult.Success);



        }
    }
}
