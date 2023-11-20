namespace BurakSekmen.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Roladı { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
