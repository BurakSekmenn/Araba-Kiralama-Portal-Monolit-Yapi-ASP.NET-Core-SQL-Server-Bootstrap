using Microsoft.AspNetCore.Identity;

namespace BurakSekmen.Localizations
{
    public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DuplicateUserName", Description = $"Bu {userName} Kullanıcı Daha önce başka bir kullanıcı tarafından alınmıştır" };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"{email} adresi başka bir kullanıcı tarafından alınmıştır." };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre En Az 6 Karakter olmalılır" };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new() { Code = "PasswordRequiresLower", Description = $"Parolada en az bir küçük harf ('a'-'z') olmalıdır!" };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new() { Code = "PasswordRequiresLower", Description = $"Parolada en az bir küçük harf ('A'-'Z') olmalıdır!" };
        }

    }
}
