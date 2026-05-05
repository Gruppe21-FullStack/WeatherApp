using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WeatherApp.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Weather> Weathers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Oslo", Latitude = 59.91, Longitude = 10.75 },
                new City { Id = 2, Name = "Stockholm", Latitude = 59.33, Longitude = 18.06 },
                new City { Id = 3, Name = "Copenhagen", Latitude = 55.67, Longitude = 12.56 },
                new City { Id = 4, Name = "Berlin", Latitude = 52.52, Longitude = 13.40 },
                new City { Id = 5, Name = "Paris", Latitude = 48.85, Longitude = 2.35 },
                new City { Id = 6, Name = "London", Latitude = 51.50, Longitude = -0.12 },
                new City { Id = 7, Name = "Madrid", Latitude = 40.41, Longitude = -3.70 },
                new City { Id = 8, Name = "Rome", Latitude = 41.90, Longitude = 12.49 },
                new City { Id = 9, Name = "Helsinki", Latitude = 60.17, Longitude = 24.94 },
                new City { Id = 10, Name = "Amsterdam", Latitude = 52.37, Longitude = 4.90 }
            );
        }
    }
}