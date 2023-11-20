namespace BurakSekmen.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PhotoUrl { get; set; }
       
        public string Role { get; set; }
    }
}
