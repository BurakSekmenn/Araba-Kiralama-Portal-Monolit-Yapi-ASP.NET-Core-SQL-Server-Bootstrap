using Microsoft.EntityFrameworkCore;

namespace BurakSekmen.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Siteseo> Siteseos { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<AracKategori> AracKategoris { get; set; }

        public DbSet<AracYakıt> AracYaks { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
