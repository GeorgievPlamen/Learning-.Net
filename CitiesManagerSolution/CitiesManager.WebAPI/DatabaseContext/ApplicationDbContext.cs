using CitiesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    CityID = Guid.Parse("C406D3CD-BB87-4A8A-9549-4CB5747F5F81"),
                    CityName = "Plovdiv"
                });
            modelBuilder.Entity<City>().HasData(
               new City()
               {
                   CityID = Guid.Parse("3EC4BB59-1D76-4A27-9140-6B6643AB3FBB"),
                   CityName = "Dimitrovgrad"
               });
        }
    }
}
