using Xunit;
using WeatherApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class CityTests
{
    // ✅ TEST 1: CREATE
    [Fact]
    public void CanAddCity()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb_Add")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Cities.Add(new City 
            { 
                Name = "TestCity", 
                Latitude = 1, 
                Longitude = 1 
            });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            Assert.Equal(1, context.Cities.Count());
        }
    }

    // ✅ TEST 2: READ
    [Fact]
    public void CanGetCities()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb_Read")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Cities.Add(new City 
            { 
                Name = "Oslo", 
                Latitude = 10, 
                Longitude = 20 
            });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var cities = context.Cities.ToList();
            Assert.Single(cities);
        }
    }

    // ✅ TEST 3: DELETE
    [Fact]
    public void CanDeleteCity()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb_Delete")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var city = new City 
            { 
                Name = "DeleteMe", 
                Latitude = 5, 
                Longitude = 5 
            };

            context.Cities.Add(city);
            context.SaveChanges();

            context.Cities.Remove(city);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            Assert.Empty(context.Cities);
        }
    }
}
