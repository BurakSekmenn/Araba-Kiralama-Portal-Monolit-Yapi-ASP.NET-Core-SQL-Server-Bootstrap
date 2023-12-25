using Microsoft.AspNetCore.Identity;

namespace BurakSekmen.Models
{
    public class User : IdentityUser
    {
       
        public string FullName { get; set; } 
       
        public string PhotoUrl { get; set; }
       

    }
}
