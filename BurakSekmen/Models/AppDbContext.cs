using Microsoft.EntityFrameworkCore;

namespace BurakSekmen.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
