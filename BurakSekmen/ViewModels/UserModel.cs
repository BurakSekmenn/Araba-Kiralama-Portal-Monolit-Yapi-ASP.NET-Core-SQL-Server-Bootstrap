namespace BurakSekmen.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public List<string> Roles { get; set; }

    }
}
